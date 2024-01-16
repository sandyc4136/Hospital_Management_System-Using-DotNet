using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.Data;
using HMS.DTO;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMS.Controllers
{
    [Authorize(Roles ="Doctor,Admin")]
    public class DoctorController : Controller
    {
        private MyDbContext _dbContext;
        private IWebHostEnvironment _webHostEnvironment;

        public DoctorController(MyDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var doc = _dbContext.Doctor;
            return View(doc);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles ="Doctor")]
        [HttpPost]
        public IActionResult Create(DoctorImage image)
        {
            string filename = Upload(image);

            if (image.ProfileImage.Length > 5 * 1024 * 1024) // 5 MB limit
            {
                // Handle the case where the file size exceeds the limit
                return BadRequest("File size exceeds the limit of 5 MB.");
            }

            var doctor = new Doctor()
            {
                FirstName = image.FirstName,
                LastName = image.LastName,
                Age = image.Age,
                Phone = image.Phone,
                Gender = image.Gender,
                BloodGroup = image.BloodGroup,
                Department = image.Department,
                ProfileImage = filename
            };

            _dbContext.Add(doctor);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = doctor.Id });
        }

        private string Upload(DoctorImage image)
        {
            string filename = null;
            if (image.ProfileImage != null)
            {
                //It is setting up the directory
                string uploaddir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                //To Generate UniqueImage Name
                filename = Guid.NewGuid().ToString() + "-" + image.ProfileImage.FileName;

                string filepath = Path.Combine(uploaddir, filename);

                using (var filestream = new FileStream(filepath, FileMode.Create))  //It will create your empty image file as per the given path
                {
                    image.ProfileImage.CopyTo(filestream);   //This will copy the stream/data of image to be uploaded to empty created file
                }

            }
            return filename;
        }

        
        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _dbContext.Doctor.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var doctorImage = new DoctorImage
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Age = doctor.Age,
                 Phone = doctor.Phone,
                Gender = doctor.Gender,
                BloodGroup = doctor.BloodGroup,
                Department = doctor.Department,
            };

            return View(doctorImage);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public IActionResult Edit(int id, DoctorImage updatedDoctor)
        {
            if (id != updatedDoctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingDoctor = _dbContext.Doctor.Find(id);
                    existingDoctor.FirstName = updatedDoctor.FirstName;
                    existingDoctor.LastName = updatedDoctor.LastName;
                    existingDoctor.Age = updatedDoctor.Age;
                    existingDoctor.Phone = updatedDoctor.Phone;
                    existingDoctor.Gender = updatedDoctor.Gender;
                    existingDoctor.BloodGroup = updatedDoctor.BloodGroup;
                    existingDoctor.Department = updatedDoctor.Department;
                    

                    _dbContext.Update(existingDoctor);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = updatedDoctor.Id });
            }

            return View(updatedDoctor);
        }

        private bool DoctorExists(int id)
        {
            return _dbContext.Doctor.Any(d => d.Id == id);
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin, Doctor")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _dbContext.Doctor.FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _dbContext.Doctor.FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        [Authorize(Roles = "Admin, Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _dbContext.Doctor.FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            _dbContext.Doctor.Remove(doctor);
            _dbContext.SaveChanges();

            return RedirectToAction("Deleted");
        }

        public IActionResult Deleted()
        {
            return View();
        }
    }
}


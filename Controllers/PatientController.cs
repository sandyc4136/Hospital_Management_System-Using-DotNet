using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.Data;
using HMS.DTO;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMS.Controllers
{
    [Authorize(Roles = "Patient,Admin")]
    public class PatientController : Controller
    {
        
        private MyDbContext _dbContext;
        private IWebHostEnvironment _webHostEnvironment;
        //private ApplicationUser applicaionUser;

        public PatientController(MyDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var patient = _dbContext.Patient;
            return View(patient);
        }


        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult Create(PatientImage image)
        {
            string filename = Upload(image);

            if (image.ProfileImage.Length > 5 * 1024 * 1024) // 5 MB limit
            {
                // Handle the case where the file size exceeds the limit
                return BadRequest("File size exceeds the limit of 5 MB.");
            }

            var patient = new Patient()
            {
                Name = image.Name,
                Phone = image.Phone,
                Disease = image.Disease,
                BloodGroup = image.BloodGroup,
                Age = image.Age,
                Gender = image.Gender,
                Address = image.Address,
                ProfileImage = filename
            };

            _dbContext.Add(patient);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = patient.Id });
        }

        private string Upload(PatientImage image)
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

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _dbContext.Patient.Find(id);

            if (patient == null)
            {
                return NotFound();
            }

            var patientImage = new PatientImage
            {
                Id = patient.Id,
                Name = patient.Name,
                //Email = patient.Email,
                Phone = patient.Phone,
                Disease = patient.Disease,
                BloodGroup = patient.BloodGroup,
                Age = patient.Age,
                Gender = patient.Gender,
                Address = patient.Address,
            };

            return View(patientImage);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult Edit(int id, PatientImage updatedPatient)
        {
            if (id != updatedPatient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPatient = _dbContext.Patient.Find(id);
                    existingPatient.Name = updatedPatient.Name;
                    //existingPatient.Email = updatedPatient.Email;
                    existingPatient.Phone = updatedPatient.Phone;
                    existingPatient.Disease = updatedPatient.Disease;
                    existingPatient.BloodGroup = updatedPatient.BloodGroup;
                    existingPatient.Age = updatedPatient.Age;
                    existingPatient.Gender = updatedPatient.Gender;
                    existingPatient.Address = updatedPatient.Address;
                    

                    _dbContext.Update(existingPatient);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = updatedPatient.Id });
            }

            return View(updatedPatient);
        }

        private bool PatientExists(int id)
        {
            return _dbContext.Patient.Any(d => d.Id == id);
        }

        [Authorize(Roles = "Admin, Patient")]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _dbContext.Patient.FirstOrDefault(d => d.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [Authorize(Roles = "Admin, Patient")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _dbContext.Patient.FirstOrDefault(d => d.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [Authorize(Roles = "Admin, Patient")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var patient = _dbContext.Patient.FirstOrDefault(d => d.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            _dbContext.Patient.Remove(patient);
            _dbContext.SaveChanges();

            return RedirectToAction("Deleted");
        }

        public IActionResult Deleted()
        {
            return View();
        }
    }
}


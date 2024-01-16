using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.Data;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMS.Controllers
{
    [Authorize(Roles = "Doctor,Admin,Patient")]
    public class BookingController : Controller
    {
        private readonly MyDbContext _context;

        public BookingController(MyDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="Admin, Doctor")]
        public async Task<IActionResult> Index()
        {
            return _context.Booking != null ?
                        View(await _context.Booking.ToListAsync()) :
                        Problem("Entity set 'MyDbContext.Booking'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var bookings = await _context.Booking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        public IActionResult Create()
        {
            return View();
        }

     


        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Patient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Name,Phone,Diseases,Age,Gender,Address,BookingDate")] Booking bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("Details", new { id = bookings.Id });
            
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var bookings = await _context.Booking.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles ="Patient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,Phone,Diseases,Age,Gender,Address,BookingDate")] Booking bookings)
        {
            if (id != bookings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(bookings.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Details", new { id = bookings.Id });
        }

        // GET: Patients/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Booking == null)
        //    {
        //        return NotFound();
        //    }

        //    var bookings = await _context.Booking
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (bookings == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bookings);
        //}

        //// POST: Patients/Delete/5
        //[Authorize(Roles = "Admin, Patient")]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Booking == null)
        //    {
        //        return Problem("Entity set 'MyDbContext.Booking'  is null.");
        //    }
        //    var bookings = await _context.Booking.FindAsync(id);
        //    if (bookings != null)
        //    {
        //        _context.Booking.Remove(bookings);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Deleted","Booking");
        //}
        [Authorize(Roles = "Admin, Patient")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = _context.Booking.FirstOrDefault(d => d.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [Authorize(Roles = "Admin, Patient")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _context.Booking.FirstOrDefault(d => d.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            _context.SaveChanges();

            return RedirectToAction("Deleted");
        }


        private bool BookingExists(int id)
        {
            return (_context.Booking?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult ThankYou()
        {
            return View("ThankYou");
        }

        public IActionResult Deleted()
        {
            return View();
        }
    }
}


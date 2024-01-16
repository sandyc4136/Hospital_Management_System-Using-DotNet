﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS.Data;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace HMS.Controllers
{
    
    public class ContactUsController : Controller
    {
        private readonly MyDbContext _context;

        public ContactUsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: ContactUs
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.ContactUs != null ? 
                          View(await _context.ContactUs.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.ContactUs'  is null.");
        }

        // GET: ContactUs/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,Message")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUs);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("ThankYou", "ContactUs");
            //return RedirectToAction("Index", "Home");
        }

        // GET: ContactUs/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }
            return View(contactUs);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,Message")] ContactUs contactUs)
        {
            if (id != contactUs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsExists(contactUs.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contactUs);
        }

        // GET: ContactUs/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // POST: ContactUs/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactUs == null)
            {
                return Problem("Entity set 'MyDbContext.ContactUs'  is null.");
            }
            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs != null)
            {
                _context.ContactUs.Remove(contactUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsExists(int id)
        {
          return (_context.ContactUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

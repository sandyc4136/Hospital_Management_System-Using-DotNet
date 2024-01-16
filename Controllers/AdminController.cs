using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HMS.DTO;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager; // object of RoleManager
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //[HttpGet]
        public async Task<IActionResult> Index()
        {

            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        //[HttpGet]
        //public IActionResult GetUsers()
        //{
        //    var users = _userManager.Users;
        //    return ViewComponent(users);
        //}

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }

        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDet(string id)
        {
            var us = await _userManager.FindByIdAsync(id);
            return View(us);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoletoUser(string id, string roleName)
        {

            var us = await _userManager.FindByIdAsync(id);

            var role = await _roleManager.FindByNameAsync(roleName);
            await _userManager.AddToRoleAsync(us, role.Name);
            return RedirectToAction("GetUsers");
        }

        //public Task<IActionResult> GetUserIdAndRoleId()
        //{

        //    return View();
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleStore roleStore)
        {
            var alreadyAdded = await _roleManager.RoleExistsAsync(roleStore.RoleName);

            if (!alreadyAdded)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleStore.RoleName));
                return RedirectToAction("Index");
            }
            return View();
        }

        
    }
}


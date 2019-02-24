using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FineArtDemo.Areas.Identity.Data;
using FineArtDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FineArtDemo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorsController : Controller
    {
        private readonly FineArtDemoContext _context;
        private readonly UserManager<CustomizeUser> _userManager;
        public AdministratorsController(UserManager<CustomizeUser> userManager, FineArtDemoContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var x = _userManager.GetUsersInRoleAsync("Administrator");
            var y = x.Result;
            ViewData["test"] = y[0].Id;
            // Get Administrator Account
            ViewData["AdminList"] = _userManager.GetUsersInRoleAsync("Administrator");

            // Get Manager Account
            ViewData["ManagerList"] = _userManager.GetUsersInRoleAsync("Manager");

            // Get Staff Account
            ViewData["StaffList"] = _userManager.GetUsersInRoleAsync("Staff");

            // Get Student Account
            ViewData["StudentList"] = _userManager.GetUsersInRoleAsync("Student");

            return View();
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountDetail = await _context.Users.FindAsync(id);
            var roleID = _context.UserRoles.Where(c=>c.UserId==id).Single();
            var role = _context.Roles.Find(roleID.RoleId);
            ViewData["AccountRole"] = role.Name;
            
            if (accountDetail == null)
            {
                return NotFound();
            }

            return View(accountDetail);
        }
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string RoleName { get; set; }
        }
        public IActionResult Create()
        {
            ViewData["RoleList"] = new SelectList(_context.Roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomizeUser { UserName = input.Email, Email = input.Email };
                var createUserResult = await _userManager.CreateAsync(user, input.Password);
                var createUserRoleResult = await _userManager.AddToRoleAsync(user, input.RoleName);
                if (createUserResult.Succeeded && createUserRoleResult.Succeeded)
                {
                    TempData["Success"] = "Created success.";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["Faile"] = "Email already taken.";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var currentRole = _context.UserRoles.Find(user.Id);
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name", currentRole.RoleId);
            return View(user);
        }
    }
}
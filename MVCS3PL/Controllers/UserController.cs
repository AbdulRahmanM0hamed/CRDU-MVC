using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_DAL.Models;
using MVC3_BLL.interfaces;
using MVC3_BLL.Repository;
using MVCS3PL.Helpers;
using MVCS3PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCS3PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchByEmail = "")
        {

            if (string.IsNullOrEmpty(SearchByEmail))
            {
                var users = await userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FName = U.FName,
                    LName = U.LName,
                    Email = U.Email,
                    PhoneNumber = U.PhoneNumber,
                    Roles = userManager.GetRolesAsync(U).Result

                }).ToListAsync();
                return View(users);
            }
            else
            {

                var users = await userManager.FindByEmailAsync(SearchByEmail);
                var MappedUser = new UserViewModel()
                {
                    Id = users.Id,
                    FName = users.FName,
                    LName = users.LName,
                    Email = users.Email,
                    PhoneNumber = users.PhoneNumber,
                    Roles = userManager.GetRolesAsync(users).Result

                };
                return View(new List<UserViewModel>() { MappedUser });
            }
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var UserVM = mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(viewName, UserVM);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel UserVM)
        {
            if (id != UserVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(UserVM.Id);
                    user.FName = UserVM.FName;
                    user.LName=UserVM.LName;
                    user.PhoneNumber = UserVM.PhoneNumber;



                    await userManager.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(String.Empty, ex.Message);
                }
            }
            return View(UserVM);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel USerVM)
        {
            if (id != USerVM.Id)
                return BadRequest();

            try
            {
                var user = await userManager.FindByIdAsync(id);
            
                await userManager.DeleteAsync(user);
              
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(USerVM);
            }
        }
















    }
}

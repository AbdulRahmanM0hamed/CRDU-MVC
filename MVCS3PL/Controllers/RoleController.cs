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
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<IdentityRole> roleManager ,IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string Name)
        {

            if (string.IsNullOrEmpty(Name))
            {
                var roles = await roleManager.Roles.Select(R => new RoleViweModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                   
                }).ToListAsync();
                return View(roles);
            }
            else
            {

                var roles = await roleManager.FindByNameAsync(Name);
                var MappedRole = new RoleViweModel()
                {
                    Id = roles.Id,
                    RoleName = roles.Name,

                };
                return View(new List<RoleViweModel>() { MappedRole });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViweModel model)
        {
            if (ModelState.IsValid)
            {//mapping
                var RoleVM = mapper.Map<RoleViweModel,IdentityRole >(model);
               
                await roleManager.CreateAsync(RoleVM);

                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var Role = await roleManager.FindByIdAsync(id);
            if (Role == null)
                return NotFound();

            var RoleVM = mapper.Map<IdentityRole, RoleViweModel>(Role);

            return View(viewName, RoleVM);

        }


        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViweModel RoleVm)
        {
            if (id != RoleVm.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await roleManager.FindByIdAsync(RoleVm.Id);
                    
                    Role.Name = RoleVm.RoleName;

                    await roleManager.UpdateAsync(Role);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(String.Empty, ex.Message);
                }
            }
            return View(RoleVm);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleViweModel RoleVM)
        {
            if (id != RoleVM.Id)
                return BadRequest();

            try
            {
                var user = await roleManager.FindByIdAsync(id);

                await roleManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(RoleVM);
            }
        }




    }
}

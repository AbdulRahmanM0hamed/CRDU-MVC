using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_DAL.Models;
using MVC3_BLL.interfaces;
using MVC3_BLL.Repository;
using MVCS3PL.Helpers;
using MVCS3PL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCS3PL.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchByName = "")
        {
            IEnumerable<Employee> employee;
            if (string.IsNullOrEmpty(SearchByName))
                employee = await unitOfWork.EmployeeRepository.GetAll();
            else
                employee = unitOfWork.EmployeeRepository.Search(SearchByName);


            var EmpVM = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);
            return View(EmpVM);


        }



        [HttpGet]
        public IActionResult Create()
        {
            // ViewBag.Departments = unitOfWork.EmployeeRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {//mapping
                var EmpVM = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                EmpVM.ImageName = DecumentSettings.UploadImage(employeeVM.Image, "image");


                await unitOfWork.EmployeeRepository.Add(EmpVM);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View(employeeVM);

        }


        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = await unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee == null)
                return NotFound();

            var EmpVM = mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(viewName, EmpVM);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //ViewBag.Departments = unitOfWork.EmployeeRepository.GetAll();

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var EmpVM = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    EmpVM.ImageName = DecumentSettings.UploadImage(employeeVM.Image, "image");

                    unitOfWork.EmployeeRepository.UpDate(EmpVM);
                    await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(String.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel _employeeVM)
        {
            if (id != _employeeVM.Id)
                return BadRequest();


            try
            {
                var EmpVM = mapper.Map<EmployeeViewModel, Employee>(_employeeVM);
                if(_employeeVM.ImageName is not null)
                {
                    DecumentSettings.DeleteFile(_employeeVM.ImageName, "image");
                }
                unitOfWork.EmployeeRepository.Delete(EmpVM);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(_employeeVM);
            }
        }





    }
}

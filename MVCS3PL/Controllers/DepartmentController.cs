using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_DAL.Models;
using MVC3_BLL.interfaces;
using MVC3_BLL.Repository;
using MVCS3PL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCS3PL.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{   //GEtAll();
			var Department = await unitOfWork.DepartmentRepository.GetAll();
			var DeptVM = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(Department);
			return View(DeptVM);
		}

		[HttpGet]

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
		{
			if (ModelState.IsValid)
			{
				var DeptVM = mapper.Map<DepartmentViewModel, Department>(departmentVM);
				await unitOfWork.DepartmentRepository.Add(DeptVM);
				int result = await unitOfWork.Complete();
				if (result > 0)
					TempData["Message"] = "Department Is created";

				ViewBag.ShowAlert = true;

				return RedirectToAction(nameof(Index));
			}
			return View(departmentVM);
		}


		public async Task<IActionResult> Details(int? id, string ViewName = "Details")
		{
			if (id is null)
				return BadRequest();
			var Department = await unitOfWork.DepartmentRepository.Get(id.Value);


			if (Department is null)
				return NotFound();

			var DeptVM = mapper.Map<Department, DepartmentViewModel>(Department);

			return View(ViewName, DeptVM);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			return await Details(id, "Edit");

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] int? id, DepartmentViewModel departmentVM)
		{
			if (id != departmentVM.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					var DeptVM = mapper.Map<DepartmentViewModel, Department>(departmentVM);

					unitOfWork.DepartmentRepository.UpDate(DeptVM);
					await unitOfWork.Complete();
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(departmentVM);

		}

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> Delete([FromRoute] int? id, DepartmentViewModel departmenVM)
		{
			if (id != departmenVM.Id)
				return BadRequest();
			try
			{
				var DeptVM = mapper.Map<DepartmentViewModel, Department>(departmenVM);

				unitOfWork.DepartmentRepository.Delete(DeptVM);
				await unitOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return View(departmenVM);
			}

		}

	}
}

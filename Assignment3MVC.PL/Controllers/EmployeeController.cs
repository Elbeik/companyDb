using Assignment3MVC.BLL.Interfaces;
using Assignment3MVC.DAL.Entities;
using Assignment3MVC.PL.Helpers;
using Assignment3MVC.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            var employees = Enumerable.Empty<Employee>();// empty sequence 
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees = await _unitOfWork._employeeRepository.GetAll();
                
            }
            else
            {
                employees = _unitOfWork._employeeRepository.GetNameOfEmplyees(SearchValue);
                
            }
            var EmployeeViewMapp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(EmployeeViewMapp);

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork._departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel emplyee)
        {
            if (ModelState.IsValid) // server side valdiation
            {

                #region Manual Mapping
                /////Manual Mapping
                //var empViewMap = new Employee()
                //{
                //    Name = emplyee.Name,
                //    Age = emplyee.Age,
                //    Address = emplyee.Address,
                //    Salary = emplyee.Salary,
                //    IsActive = emplyee.IsActive,
                //    DepartmentId = emplyee.DepartmentId,
                //    Email = emplyee.Email,
                //    PhoneNumber = emplyee.PhoneNumber,

                //}; 
                #endregion
                emplyee.ImageName =  DocumentSettings.UploadFile(emplyee.Img, "Images");
                /// Auto Mapp
                var empViewMap = _mapper.Map<EmployeeViewModel, Employee>(emplyee);
                await _unitOfWork._employeeRepository.Add(empViewMap);
                return RedirectToAction(nameof(Index));
            }
            return View(emplyee);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                NotFound();
            var employee = await _unitOfWork._employeeRepository.Get(id.Value);
            if (employee == null)
                NotFound();

            var employeeViewModel = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, employeeViewModel);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    await _unitOfWork._employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(employeeViewModel);
                }

            }
            return View(employeeViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mapViewEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    var count = await _unitOfWork._employeeRepository.Delete(mapViewEmp);
                    if (count > 0)
                        DocumentSettings.DeleteFile(employeeViewModel.ImageName, "Images");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(employeeViewModel);
                }
            }
            return View(employeeViewModel);
        }
    }
}

using Assignment3MVC.BLL.Interfaces;
using Assignment3MVC.BLL.Repositories;
using Assignment3MVC.DAL.Entities;
using System;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections;
using System.Collections.Generic;
using Assignment3MVC.PL.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchName)
        {

            #region Bindding
            // Binding 
            // 1. ViewData --> from Action to view of the Same Action.
            ViewData["Message"] = "hello view data";

            //2. ViewBag --> Dynamic proparty , from Action to view of the Same Action.
            ViewBag.message = "hello view Bag";
            #endregion
            var departments = Enumerable.Empty<Department>();
            if (string.IsNullOrEmpty(SearchName))
            {
                 departments = await _unitOfWork._departmentRepository.GetAll();
            }
            else
            {
                departments = _unitOfWork._departmentRepository.searchDepartmentByName(SearchName);
            }
            var departmentViewModel = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(departmentViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid) // server side valdiation
            {
                var departmentViewModel = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                await _unitOfWork._departmentRepository.Add(departmentViewModel);
                TempData["message"] = "Department Created Successfully "; // from action to Action
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                NotFound();
            var department = await _unitOfWork._departmentRepository.Get(id.Value);
            if (department == null)
                NotFound();
            var departmentViewModel = _mapper.Map<Department, DepartmentViewModel>(department); 
            return View(ViewName, departmentViewModel);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDB = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    await _unitOfWork._departmentRepository.Update(departmentDB);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(departmentVM);
                }

            }
            return View(departmentVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDB = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    await _unitOfWork._departmentRepository.Delete(departmentDB);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(departmentVM);
                }
            }
            return View(departmentVM);
        }





        //private readonly IDepartmentRepository _departmentRepository;

        //public DepartmentController(IDepartmentRepository departmentRepository)
        //{
        //    _departmentRepository = departmentRepository;
        //}

        //public IActionResult Index()
        //{
        //    var departments = _departmentRepository.GetAll();
        //    return View(departments);
        //}
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Department department)
        //{
        //    if(ModelState.IsValid) // server side valdiation
        //    {
        //        _departmentRepository.Add(department);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(department);
        //}

        //public IActionResult Details(int? id, string  ViewName= "Details")
        //{
        //    if (id == null)
        //        NotFound();
        //    var department = _departmentRepository.Get(id.Value);
        //    if (department == null)
        //        NotFound();
        //    return View(ViewName,department);
        //}
        //public IActionResult Edit(int? id)
        //{
        //    return Details(id, "Edit");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute]int id, Department department)
        //{
        //    if(id != department.Id)
        //        return BadRequest();
        //    if(ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _departmentRepository.Update(department);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch(Exception ex)
        //        {
        //            ModelState.AddModelError("",ex.Message);
        //            return View(department);
        //        }

        //    }
        //    return View(department);
        //}

        //public IActionResult Delete(int? id)
        //{
        //    return Details(id, "Delete");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete([FromRoute]int id, Department department)
        //{
        //    if (id != department.Id)
        //        return BadRequest();
        //    if(ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _departmentRepository.Delete(department);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch(Exception ex)
        //        {
        //            ModelState.AddModelError("", ex.Message);
        //            return View(department);
        //        }
        //    }
        //    return View(department);
        //}



    }
}

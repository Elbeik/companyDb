using Assignment3MVC.DAL.Entities;
using Assignment3MVC.PL.Helpers;
using Assignment3MVC.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Assignment3MVC.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

        #region Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            var users = Enumerable.Empty<ApplicationUser>().ToList();// empty sequence 
            if (string.IsNullOrEmpty(SearchValue))
            {
                users.AddRange(_userManager.Users);

            }
            else
            {
                users.Add(await _userManager.FindByNameAsync(SearchValue));

            }

            return View(users);

        }
        #endregion

        #region Create
        //public async Task<IActionResult> Create()
        //{
        //    ViewBag.Departments = await _unitOfWork._departmentRepository.GetAll();
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(EmployeeViewModel emplyee)
        //{
        //    if (ModelState.IsValid) // server side valdiation
        //    {

        //        #region Manual Mapping
        //        /////Manual Mapping
        //        //var empViewMap = new Employee()
        //        //{
        //        //    Name = emplyee.Name,
        //        //    Age = emplyee.Age,
        //        //    Address = emplyee.Address,
        //        //    Salary = emplyee.Salary,
        //        //    IsActive = emplyee.IsActive,
        //        //    DepartmentId = emplyee.DepartmentId,
        //        //    Email = emplyee.Email,
        //        //    PhoneNumber = emplyee.PhoneNumber,

        //        //}; 
        //        #endregion
        //        emplyee.ImageName = DocumentSettings.UploadFile(emplyee.Img, "Images");
        //        /// Auto Mapp
        //        var empViewMap = _mapper.Map<EmployeeViewModel, Employee>(emplyee);
        //        await _unitOfWork._employeeRepository.Add(empViewMap);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(emplyee);
        //} 
        #endregion

        #region Details
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                NotFound();
            return View(ViewName, user);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.UserName = applicationUser.UserName;
                    user.PhoneNumber = applicationUser.PhoneNumber;

                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(applicationUser);
                }

            }
            return View(applicationUser);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, ApplicationUser deletUser)
        {
            if (id != deletUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var userDelet = await _userManager.FindByIdAsync(id);
                    await _userManager.DeleteAsync(userDelet);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(deletUser);
                }
            }
            return View(deletUser);
        } 
        #endregion
    }
}

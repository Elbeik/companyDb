using Assignment3MVC.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Assignment3MVC.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        #region Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            var roles = Enumerable.Empty<IdentityRole>().ToList();// empty sequence 
            if (string.IsNullOrEmpty(SearchValue))
            {
                roles.AddRange(_roleManager.Roles);

            }
            else
            {
                roles.Add(await _roleManager.FindByNameAsync(SearchValue));

            }

            return View(roles);

        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if (ModelState.IsValid) // server side valdiation
            {

             
               await _roleManager.CreateAsync(identityRole);
                return RedirectToAction(nameof(Index));
            }
            return View(identityRole);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                NotFound();
            return View(ViewName, role);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, IdentityRole identityRole)
        {
            if (id != identityRole.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = identityRole.Name;
                    

                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(identityRole);
                }

            }
            return View(identityRole);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, IdentityRole identityRole)
        {
            if (id != identityRole.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var userDelet = await _roleManager.FindByIdAsync(id);
                    await _roleManager.DeleteAsync(userDelet);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(identityRole);
                }
            }
            return View(identityRole);
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;   
        }
        public IActionResult Index()
        {
            return View(_userService.GetAll());
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            User? user = _userService.Get((int)id);
            if (user == null) { return NotFound(); }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            User? user = _userService.Get((int)id);
            if (user == null)
            {
                return NotFound();
            }

            TempData["success"] = _userService.Delete(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                User user = new();
                return View(user);
            }
            else
            {
                User? user = _userService.Get((int)id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
        }
        [HttpPost]
        public IActionResult Upsert(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    TempData["success"] = _userService.Create(user);
                }
                else
                {
                    TempData["success"] = _userService.Update(user);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }
    }
}

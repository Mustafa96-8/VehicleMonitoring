using Microsoft.AspNetCore.Mvc;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Areas.Admin.Controllers
{
    public class VehilceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehilceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var objVehicleList = _unitOfWork.Vehicle.GetAll().ToList();
            return View(objVehicleList);
        }

        [HttpGet]
        public IActionResult Details(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Vehicle? vehicleFromDb = _unitOfWork.Vehicle.Get(u=>u.Id == id);
            if (vehicleFromDb == null)
            {
                return NotFound();
            }
            return View();
        }

        public IActionResult UpSert()
        {

            return View();
        }
    }
}

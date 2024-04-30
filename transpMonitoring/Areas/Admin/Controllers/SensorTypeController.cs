using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class SensorTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SensorTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objSensorTypeList = _unitOfWork.SensorType.GetAll().ToList();
            return View(objSensorTypeList);
        }
    }
}

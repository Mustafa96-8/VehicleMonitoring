using Microsoft.AspNetCore.Mvc;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Areas.Admin.Controllers
{
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

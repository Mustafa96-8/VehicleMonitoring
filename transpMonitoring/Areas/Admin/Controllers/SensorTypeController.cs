using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Controllers;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class SensorTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SensorTypeController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            ICollection<SensorType> objSensorTypeList = _unitOfWork.SensorType.GetAll().ToList();
            return View(objSensorTypeList);
        }
    }
}

using Microsoft.CodeAnalysis.CSharp.Syntax;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services
{
    public class VehicleDescriptionService : IVehicleDescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleDescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Create(VehicleDescription vehicleDescription)
        {
            vehicleDescription.Vehicle = _unitOfWork.Vehicle.Get(u => u.Id == vehicleDescription.VehicleId);
            _unitOfWork.VehicleDescription.Add(vehicleDescription);
            _unitOfWork.Save();
            return "Описание успешно создано";
        }

        public string Delete(VehicleDescription vehicleDescription)
        {
            _unitOfWork.VehicleDescription.Delete(vehicleDescription);
            _unitOfWork.Save();
            return "Описание успешно удалёно";
        }

        public VehicleDescription Get(int id)
        {
            VehicleDescription? vehicleDescription = _unitOfWork.VehicleDescription.Get(u => u.Id == id);
            if (vehicleDescription == null)
            {
                return null;
            }
            return vehicleDescription;
        }

        public IEnumerable<VehicleDescription> GetAll()
        {
            return _unitOfWork.VehicleDescription.GetAll().ToList();
        }

        public List<VehicleDescription> GetByVehicleId(int vehicleId)
        {
            if (vehicleId == null || vehicleId == 0)
            { return null; }
            return _unitOfWork.VehicleDescription.GetAll().Where(n=>n.VehicleId==vehicleId).ToList();
        }

        public string Update(VehicleDescription vehicleDescription)
        {
            vehicleDescription.Vehicle = _unitOfWork.Vehicle.Get(u => u.Id == vehicleDescription.VehicleId);
            _unitOfWork.VehicleDescription.Update(vehicleDescription);
            _unitOfWork.Save();
            return "Описание успешно обновлено";
        }
        public VehicleDescriptionVM CreateVM (VehicleDescription vehicleDescription)
        {
            VehicleDescriptionVM vehicleDescriptionVM = new VehicleDescriptionVM
            {
                VehicleDescription = vehicleDescription,
                vehicleList = _unitOfWork.Vehicle.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Model +" | "+ u.StateRegisterNumber,
                    Value = u.Id.ToString(),
                }),
            };
            return vehicleDescriptionVM;
        }
    }
}

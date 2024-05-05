using Microsoft.CodeAnalysis.CSharp.Syntax;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Services
{
    public class VehicleDescriptionService : IVehicleDescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleDescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Create(VehicleDescription obj)
        {
            throw new NotImplementedException();
        }

        public string Delete(VehicleDescription obj)
        {
            throw new NotImplementedException();
        }

        public VehicleDescription Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleDescription> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VehicleDescription> GetByVehicleId(int vehicleId)
        {
            if (vehicleId == null || vehicleId == 0)
            { return null; }
            return _unitOfWork.VehicleDescription.GetAll().Where(n=>n.VehicleId==vehicleId).ToList();
        }

        public string Update(VehicleDescription obj)
        {
            throw new NotImplementedException();
        }
    }
}

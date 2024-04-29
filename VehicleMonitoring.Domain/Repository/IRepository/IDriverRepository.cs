using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface IDriverRepository:IRepository<Driver>
    {
        void Update(Driver driver);
    }
}

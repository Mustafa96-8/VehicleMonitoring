using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface IDriverRepository:IRepository<Driver>
    {
        void Update(Driver driver);
    }
}

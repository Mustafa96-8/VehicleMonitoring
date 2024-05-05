using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IService<T> where T : class
    {
        string Delete(T obj);
        T Get(int id);
        IEnumerable<T> GetAll();
        string Create(T obj);
        string Update(T obj);
    }
}

using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface IMessageRepository : IRepository<Message>
    {
        void Update(Message message);
    }
}

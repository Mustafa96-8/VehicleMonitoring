using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IMessageService:IService<Message>
    {

        MessageVM CreateVM(Message message);
    }
}

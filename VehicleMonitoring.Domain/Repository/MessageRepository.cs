using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class MessageRepository:Repository<Message>,IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void Update(Message message)
        {
            _context.Messages.Update(message);
        }
    }
}

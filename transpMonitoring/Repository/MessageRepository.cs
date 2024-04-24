using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
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

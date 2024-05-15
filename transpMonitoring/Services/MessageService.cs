using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Create(Message message)
        {
            if (message.ReportId != null)
            {
                Report? report = _unitOfWork.Report.Get(u => u.Id == message.ReportId);
                if (report == null) { return "Указанный отчёт не найден"; }
                report.Messages.ToList().Add(message);
                _unitOfWork.Report.Update(report);
            }
            _unitOfWork.Message.Add(message);
            _unitOfWork.Save();
            return "Сообщение успешно создано";
        }

        public string Delete(Message message)
        {
            _unitOfWork.Message.Delete(message);
            _unitOfWork.Report.Get(u => u.Id == message.ReportId).Messages.ToList().Remove(message);
            _unitOfWork.Save();
            return "Сообщение успешно удалёно";
        }

        public Message? Get(int id)
        {
            return _unitOfWork.Message.Get(u => u.Id == id);
        }

        public IEnumerable<Message> GetAll()
        {
            return _unitOfWork.Message.GetAll().ToList();
        }

        public string Update(Message message)
        {
            if (message.ReportId != null)
            {
                Report? report = _unitOfWork.Report.Get(u => u.Id == message.ReportId);
                if (report == null) { return "Указанный отчёт не найден"; }
                _unitOfWork.Report.Update(report);
            }
            _unitOfWork.Message.Update(message);
            _unitOfWork.Save();
            return "Сообщение успешно обновлено";
        }
        public MessageVM CreateVM(Message message)
        {
            MessageVM messageVM = new MessageVM
            {
                Message = message,
                ReportList = _unitOfWork.Report.GetAll().Select(u => new SelectListItem
                {
                    Text = u.VehicleId + " | " + u.CreationTime,
                    Value = u.Id.ToString(),
                }),

            };
            return messageVM;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    public class MessageController : BaseController
    {
        readonly public IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_messageService.GetAll());
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Message? message = _messageService.Get((int)id);
            if (message == null) { return NotFound(); }
            return View(message);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Message? message = _messageService.Get((int)id);
            if (message == null) { return NotFound(); }
            return View(message);
        }
        [HttpDelete, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            Message? message = _messageService.Get((int)id);
            if (message == null)
            {
                return NotFound();
            }

            TempData["success"] = _messageService.Delete(message);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            MessageVM messageVM;
            if (id == null || id == 0)
            {
                Message message = new();
                messageVM = _messageService.CreateVM(message);
            }
            else
            {
                Message? message = _messageService.Get((int)id);
                if (message == null)
                {
                    return NotFound();
                }
                messageVM = _messageService.CreateVM(message);

            }
            return View(messageVM);
        }
        [HttpPost]
        public IActionResult Upsert(MessageVM messageVM)
        {
            if (ModelState.IsValid)
            {
                if (messageVM.Message.Id == 0)
                {
                    TempData["success"] = _messageService.Create(messageVM.Message);
                }
                else
                {
                    TempData["success"] = _messageService.Update(messageVM.Message);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(_messageService.CreateVM(messageVM.Message));
            }
        }
    }
}

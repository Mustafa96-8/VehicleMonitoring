using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        readonly public IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_reportService.GetAll());
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Report? report = _reportService.Get((int)id);
            if (report == null) { return NotFound(); }
            return View(report);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Report? report = _reportService.Get((int)id);
            if (report == null) { return NotFound(); }
            return View(report);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            Report? report = _reportService.Get((int)id);
            if (report == null)
            {
                return NotFound();
            }

            TempData["success"] = _reportService.Delete(report);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ReportVM reportVM;
            if (id == null || id == 0)
            {
                Report report = new();
                reportVM = _reportService.CreateVM(report);
            }
            else
            {
                Report? report = _reportService.Get((int)id);
                if (report == null)
                {
                    return NotFound();
                }
                reportVM = _reportService.CreateVM(report);

            }
            return View(reportVM);
        }
        [HttpPost]
        public IActionResult Upsert(ReportVM reportVM)
        {
            if (ModelState.IsValid)
            {
                if (reportVM.Report.Id == 0)
                {
                    TempData["success"] = _reportService.Create(reportVM.Report);
                }
                else
                {
                    TempData["success"] = _reportService.Update(reportVM.Report);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(_reportService.CreateVM(reportVM.Report));
            }
        }
    }
}

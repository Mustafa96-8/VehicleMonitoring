using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.ViewModels
{
    public class HomePartialVM
    {
        public HomePartialVM(Report report, Vehicle vehicle)
        {
            Report = report;
            Vehicle = vehicle;

        }
        public Vehicle Vehicle { get; set; }
        public Report Report { get; set; }
    }
}

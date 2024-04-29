using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IDriverRepository Driver { get; private set; }
        public IGPSDataRepository GPSData { get; private set; }
        public IGPSReadingRepository GPSReading { get; private set; }
        public IReportRepository Report {  get; private set; }
        public IMessageRepository Message { get; private set; }
        public ISensorRepository Sensor { get; private set; }
        public ISensorTypeRepository SensorType { get; private set; }
        public ISensorValueRepository SensorValue { get; private set; }
        public IVehicleRepository Vehicle { get; private set; }
        public IVehicleDescriptionRepository VehicleDescription { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Driver = new DriverRepository(_context);
            GPSData = new GPSDataRepository(_context);
            GPSReading = new GPSReadingRepository(_context);
            Report = new ReportRepository(_context);
            Message = new MessageRepository(_context);
            Sensor = new SensorRepository(_context);
            SensorType = new SensorTypeRepository(_context);
            SensorValue = new SensorValueRepository(_context);
            Vehicle = new VehicleRepository(_context);
            VehicleDescription = new VehicleDescriptionRepository(_context);
            
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

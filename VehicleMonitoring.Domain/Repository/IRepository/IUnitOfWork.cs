﻿namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDriverRepository Driver { get; }
        IGPSDataRepository GPSData { get; }
        IGPSReadingRepository GPSReading { get; }
        IMessageRepository Message { get; }
        IReportRepository Report { get; }
        ISensorRepository Sensor { get; }
        ISensorTypeRepository  SensorType { get; }
        ISensorValueRepository SensorValue { get; }
        IUserRepository User { get; }
        IVehicleRepository Vehicle { get; }
        IVehicleDescriptionRepository VehicleDescription { get; }


        void Save();
    }
}

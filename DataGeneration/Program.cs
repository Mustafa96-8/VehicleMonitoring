using System;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Extensions;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;

namespace SensorDataApp
{
    class Program
    {

        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                IUnitOfWork unitOfWork = new UnitOfWork(context);
                ReportHandler reportHandler= new ReportHandler(unitOfWork);

                var vehicleList = context.Vehicles.ToList();
                foreach (var vehicle in vehicleList)
                {
                    reportHandler.GenerateReport(vehicle.Id);
                }

                SeedData(context);

                
            }
            Console.WriteLine("Sensor values added successfully!");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(
                            hostContext.Configuration.GetConnectionString("WebApiDatabase")));
                });

        private static void SeedData(ApplicationDbContext context)
        {
            var sensors = context.Sensors.ToList();

            foreach (var sensor in sensors)
            {
                var sensorValues = GenerateSensorValues(sensor, context);
                context.SensorValues.AddRange(sensorValues);
            }

            context.SaveChanges();
        }

        static SensorValue[] GenerateSensorValues(Sensor sensor, ApplicationDbContext context)
        {
            var faker = new Faker();
            var lastValue = context.SensorValues
                .Where(u => u.SensorId == sensor.Id)
                .OrderByDescending(u => u.CreationTime)
                .FirstOrDefault()?.Value ?? 0;

            double errorValue = faker.Random.Double(1.2,1.4);
            double newValue = 0;
            switch (sensor.Name)
            {
                case "Давление колеса 1":
                case "Давление колеса 2":
                case "Давление колеса 3":
                case "Давление колеса 4":
                    newValue = faker.Random.Double(sensor.ParametrLower.Value, sensor.ParametrUpper.Value * errorValue);
                    break;
                case "Температура масла":
                    var speed = context.SensorValues
                        .Where(u => u.SensorId == 6)
                        .OrderByDescending(u => u.CreationTime)
                        .FirstOrDefault()?.Value ?? 0;
                    newValue = faker.Random.Double(sensor.ParametrLower.Value+30, sensor.ParametrUpper.Value * errorValue) + (speed / 80);
                    break;

                case "Зажигание":
                    //newValue = faker.Random.Int(0, 1);
                    newValue = 0;
                    break;

                case "Скорость по спидометру":
                    var ignition = context.SensorValues
                        .Where(u => u.SensorId == 5)
                        .OrderByDescending(u => u.CreationTime)
                        .FirstOrDefault()?.Value ?? 0;
                    if (ignition == 1)
                    {
                        newValue = faker.Random.Double(20, sensor.ParametrUpper * errorValue ?? 200);
                    }
                    else
                    {
                        newValue = 0;
                    }
                    break;

                case "Одометр":
                    speed = context.SensorValues
                        .Where(u => u.SensorId == 6)
                        .OrderByDescending(u => u.CreationTime)
                        .FirstOrDefault()?.Value ?? 0;
                    newValue = lastValue + speed * 0.01;
                    break;

                case "Уровень топлива в баке 1":
                    /*ignition = context.SensorValues
                        .Where(u => u.SensorId == 5)
                        .OrderByDescending(u => u.CreationTime)
                        .FirstOrDefault()?.Value ?? 0;*/

                    double difference = faker.Random.Double(sensor.ParametrUpper*errorValue??0.1, sensor.ParametrUpper * errorValue??0.1*errorValue);
                        newValue = lastValue - difference;
                    break;
            }

            return
            [
                new SensorValue
                {
                    Value = newValue,
                    SensorId = sensor.Id
                }
            ];
        }
    }
}

using System;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;

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

                /*ReportService reportService = services.GetRequiredService<ReportService>();
                var vehicleList = context.Vehicles.ToList();
                foreach (var vehicle in vehicleList)
                {
                    reportService.ReportHandler.GenerateReport(vehicle.Id);
                }*/

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
                    /*services.AddScoped<ReportService>();*/
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
                    newValue = faker.Random.Double(sensor.ParametrLower.Value, sensor.ParametrUpper.Value*errorValue);
                    break;
                case "Давление колеса 2":
                    newValue = faker.Random.Double(sensor.ParametrLower.Value, sensor.ParametrUpper.Value * errorValue);
                    break;
                case "Давление колеса 3":
                    newValue = faker.Random.Double(sensor.ParametrLower.Value, sensor.ParametrUpper.Value * errorValue);
                    break;
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
                        newValue = faker.Random.Double(0, sensor.ParametrUpper * errorValue ?? 200);
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
                    ignition = context.SensorValues
                        .Where(u => u.SensorId == 5)
                        .OrderByDescending(u => u.CreationTime)
                        .FirstOrDefault()?.Value ?? 0;
                    if (ignition == 1)
                    {
                        newValue = lastValue - 0.01;
                        newValue = Math.Max(newValue, 0.05) * errorValue;
                    }
                    break;
            }

            return new SensorValue[]
            {
                new SensorValue
                {
                    Value = newValue,
                    SensorId = sensor.Id
                }
            };
        }
    }
}

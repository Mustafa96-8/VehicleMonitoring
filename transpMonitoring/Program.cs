using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.Domain.Repository;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql
    (builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISensorTypeService, SensorTypeService>();
builder.Services.AddScoped<IVehicleService,VehicleService>();
builder.Services.AddScoped<IDriverService,DriverService>();
builder.Services.AddScoped<IVehicleDescriptionService,VehicleDescriptionService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Customer/Account");
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

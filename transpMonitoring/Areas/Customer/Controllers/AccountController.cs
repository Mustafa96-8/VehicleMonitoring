using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Customer.Controllers
{

    public class AccountController : BaseController
    {
        ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoginAsync([Bind(Prefix = "l")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = model
                });
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
            
            if ((user is null )|| (!Encryption.Equals(model.Password, user.PasswordHash, user.Salt)))
            {
                TempData["error"] = "Неверный логин или пароль";
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = model
                });
            }
            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        private async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> RegisterAsync([Bind(Prefix = "r")] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel
                {
                    RegisterViewModel = model
                });
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);

            if (user != null)
            {
                TempData["error"] = "Пользователь с таким логином уже существует";
                return View("Index", new AccountViewModel
                {
                    RegisterViewModel = model
                });
            }
            string salt = Encryption.CreateSalt(16);
            user = new User {Login= model.Login,PasswordHash= Encryption.GenerateHash(model.Password, salt),Salt= salt, Role=model.Role,FirstName= model.FirstName,LastName= model.LastName };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await AuthenticateAsync(user);

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        internal static class Encryption
        {
            public static string CreateSalt(int size)
            {
                //Generate a cryptographic random number.
                byte[] buff = new byte[size];
                RandomNumberGenerator rng = RandomNumberGenerator.Create();
                rng.GetBytes(buff);
                return Convert.ToBase64String(buff);
            }

            public static string GenerateHash(string input, string salt)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
                SHA256 sha = SHA256.Create();
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }

            public static bool Equals(string plainTextInput, string hashedInput, string salt)
            {
                string newHashedPin = GenerateHash(plainTextInput, salt);
                return newHashedPin.Equals(hashedInput);
            }
        }
    }
}

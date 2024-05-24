using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Extensions;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string Create(User model)
        {
            string salt = Encryption.CreateSalt(16);

            var user = new User 
            { 
                Login = model.Login, 
                PasswordHash = Encryption.GenerateHash(model.PasswordHash, salt), 
                Salt = salt, 
                Role = model.Role, 
                FirstName = model.FirstName, 
                LastName = model.LastName 
            };
            _unitOfWork.User.Add(user);
            _unitOfWork.Save();
            return "user Created Successfully";
        }


        public string Delete(User user)
        {
            _unitOfWork.User.Delete(user);
            IVehicleService vehicleService = new VehicleService(_unitOfWork);
            foreach(var obj in _unitOfWork.Vehicle.GetAll().Where(u=>u.UserId==user.Id))
            {
                vehicleService.Delete(obj);
            }
            _unitOfWork.Save();
            return "Пользователь удалён с его техникой?)";
        }

        public User? Get(int id)
        {
            User? user = _unitOfWork.User.Get(u => u.Id == id);
            user.Vehicles = _unitOfWork.Vehicle.GetAll().Where(u=>u.UserId==user.Id).ToList();
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.User.GetAll();
        }

        public string Update(User user)
        {
            User _user = _unitOfWork.User.Get(u => u.Id == user.Id);

            _user.Login = user.Login;
            _user.Role = user.Role;
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            if ((_user.PasswordHash != user.PasswordHash || _user.Salt != user.Salt) && (!string.IsNullOrEmpty(user.PasswordHash)))
            {
                string salt = Encryption.CreateSalt(16);
                _user.PasswordHash = Encryption.GenerateHash(user.PasswordHash, salt);
                _user.Salt = salt;
            }
            _unitOfWork.User.Update(_user);
            _unitOfWork.Save();
            return "Информация о пользователе успешно обновлёна";
        }
        
    }
}

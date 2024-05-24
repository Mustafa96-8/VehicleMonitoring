using System.ComponentModel.DataAnnotations;

namespace VehicleMonitoring.mvc.ViewModels
{
    public class AccountVM
    {
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Password { get; set; } 
    }


    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Login { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Role { get; set; } = "user";

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(8),MaxLength(30)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Compare("Password",ErrorMessage ="Пароли не совпадают") ]
        public string RepeatPassword { get; set; }
    }
}

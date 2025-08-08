using System.ComponentModel.DataAnnotations;

namespace Chat_SignalR.Models.ApiModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Пароль введен неверно")]
        public string confirmPassword { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public LoginModel(RegisterModel registerModel) {
            this.name = registerModel.name;
            this.password = registerModel.password;
        }
        /// <summary>
        /// пустой конструктор 
        /// </summary>
        public LoginModel()
        {

        }
    }
}

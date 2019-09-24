using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wahama.ViewModels
{
    public class RegisterModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "E - mail введён неверно")]
        [Required(ErrorMessage = "E-mail введён неверно")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Имя не указано")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия не указана")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [RegularExpression(@"^((8 |\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Номер введён неверно")]
        [Required(ErrorMessage = "Номер телефона указан не верно")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public DateTime ExpirationDate { get; set; }

    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан E-mail")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}

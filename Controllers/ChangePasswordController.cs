using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Wahama.Models;
using System.IO;

namespace Wahama.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly WarhammerContext _context;
        public ChangePasswordController(WarhammerContext context)
        {
            _context = context;
        }
        [HttpPost]
        public void SendMessage(string email)
        {
            MailController mc = new MailController();
            mc.SendMessage(new MailMessage
            {
                MessageText = new MimeKit.TextPart("plain") { Text = $"Ваш код потверждения для восстановления пароля: {ActivateAndReturnGuid(email)}" },
                Subject = "Восстановление пароля",
                ToAddress = email,
                ToName = email.Split('@')[0]
            });
        }

        public string ActivateAndReturnGuid(string email) // создается токен с сроком действия 15 минут
        {
            Guid guid = Guid.NewGuid();
            _context.TokenList.Add(new TokenList { Guid = guid.ToString(), Login = email, ExpirationDate = DateTime.Now.AddMinutes(15) });
            _context.SaveChanges();
            return guid.ToString();
        }

        #region HashSalting

        [HttpPost]
        public bool CompareToken(string mail, string password)
        {
            if (IsValid(mail))
            {
                string savedPassword = _context.TokenList.Where(u => u.Login == mail).Select(p => p.Guid).Last();
                if (password == savedPassword)
                {
                    return true;
                }
                return false;

            }
            return false;
        }

        public bool IsValid(string email)
        {
            var guid = _context.TokenList.Where(p => p.Login == email).LastOrDefault();
            if (DateTime.Now >= guid.ExpirationDate)
            {
                return false;
            }
            return true;
        }
        #endregion

        public void MailPasswordNotification(string mail)
        {
            User user = _context.Users.Where(p => p.Login == mail).FirstOrDefault();
            MailController mc = new MailController();
            mc.SendMessage(new MailMessage
            {
                ToAddress = user.Login,
                ToName = user.FirstName,
                Subject = "Ваш пароль был изменён",
                MessageText = new TextPart("html")
                {
                    Text = "<div style='text-align:center'>" +
                    "<h2>Wahama Shop</h2>" +
                    "<hr />" +
                    "<h3>Здравствуйте! Ваш пароль был успешно восстановлен. </h3>" +
                    "<div><h4>Если это действие совершали не Вы, свяжитесь с службой поддержки Wahama Shop.</div>" +
                    "<img src='https://i.kym-cdn.com/photos/images/facebook/001/337/806/df6.png' style='width:250px'/>" +
                    "</div>"
                }
            }) ;
        }
        [HttpPost]
        public void UpdatePassword(string newPassword, string mail)
        {
            using (AccountController accountController1 = new AccountController(_context))
            {
                User user = _context.Users.Where(p => p.Login == mail).FirstOrDefault();
                user.Password = accountController1.HashPassword(newPassword);
                _context.Users.Update(user);
                _context.SaveChanges();
                MailPasswordNotification(mail);
            }
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
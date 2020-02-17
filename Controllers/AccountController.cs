using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wahama.Models;
using Wahama.ViewModels;
using MimeKit;

namespace Wahama.Controllers
{
    public class AccountController : Controller
    {
        private readonly WarhammerContext _context;
        public AccountController(WarhammerContext context)
        {
            _context = context;
        }

        #region Views
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AccountInfo() // Настройки аккунта
        {
            return View(_context.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault());
        }

        public IActionResult RestorePassword()
        {
            return View();
        }

        #endregion

        #region HashSalting
        public string HashPassword(string password)
        {
            byte[] salt; // Generating Salt
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);            
            return Convert.ToBase64String(hashBytes);            
        }

        public bool ComparePassword(string login, string password)
        {
            string savedPasswordHash = _context.Users.Where(u => u.Login == login).Select(p => p.Password).FirstOrDefault();
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;

        }
        #endregion

        #region Authentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, bool isTemporary = false)
        {
            if (ModelState.IsValid)
            {
                switch (isTemporary) // Проверка на то, временный пользователь должен быть создан или нет
                {
                    case false:
                        {
                            User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                            if (user == null)
                            {
                                // добавляем пользователя в бд

                                user = new User
                                {
                                    Login = model.Login,
                                    Password = HashPassword(model.Password),
                                    FirstName = model.FirstName,
                                    LastName = model.LastName,
                                    PhoneNumber = model.PhoneNumber
                                };
                                Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                                if (userRole != null)
                                    user.Role = userRole;

                                _context.Users.Add(user);
                                await _context.SaveChangesAsync();

                                await Authenticate(user, false); // аутентификация

                                return RedirectToAction("Index", "Home");
                            }
                            else
                                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                            break;
                        }
                    case true:
                        {
                            User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                            if (user == null)
                            {

                                user = new User
                                {
                                    Login = model.Login,
                                    Password = HashPassword(model.Password),
                                    FirstName = model.FirstName,
                                    LastName = model.FirstName,
                                    PhoneNumber = model.PhoneNumber,
                                    ExpirationDate = model.ExpirationDate
                                };
                                Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                                if (userRole != null)
                                    user.Role = userRole;
                                _context.Users.Add(user);
                                await _context.SaveChangesAsync();

                                return RedirectToAction("Index", "Home");
                            }
                            else
                                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                            break;
                        }
                }

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user != null)
                {
                    if (ComparePassword(user.Login, model.Password))
                    {
                        await Authenticate(user, model.RememberMe); // аутентификация                    
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user, bool isPersistent)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            if (isPersistent)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id), new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddYears(1)
                });
            }
            else
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public void UpdatePhoneNumber(Phone phone) // Вызывается только из настроек аккаунта
        {
            var account = _context.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault();
            account.PhoneNumber = phone.Number;
            _context.Users.Update(account);
            _context.SaveChangesAsync();
        }
        

        public void MailPasswordNotification(string mail)
        {
            User user = _context.Users.Where(p => p.Login == mail).FirstOrDefault();
            MailController mc = new MailController();
            mc.SendMessage(new MailMessage
            {
                ToAddress = User.Identity.Name,
                ToName = user.FirstName,
                Subject = "Ваш пароль был изменён",
                MessageText = new TextPart("html")
                {
                    Text = "<h2>Wahama Shop</h2> " +
                "<h4>Ваш пароль был успешно изменён.</h4>" +
                "<h5>Если это сделали не Вы, обратитесь в службу поддержки.</h5>"
                }
            });
        }
        [HttpPost]
        public IActionResult ChangePassword (UpdatePassword pw)
        {
            if ((ComparePassword(User.Identity.Name, pw.OldPassword)) && pw.NewPassword == pw.NewPasswordConfirmation)
            {
                
                User user = _context.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault();
                user.Password = HashPassword(pw.NewPassword);
                _context.Users.Update(user);
                _context.SaveChanges();
                MailPasswordNotification(User.Identity.Name);
                return RedirectToAction("AccountInfo");
            }
            else
            {
                ModelState.AddModelError("", "Данные введены неверно");
            }
            return View();
        }
        #endregion 

    }
}

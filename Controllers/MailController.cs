﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using Wahama.Models;

namespace Wahama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        [HttpPost]
        public void SendMessage(MailMessage mailMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Wahama-Shop", "wahama.shop@gmail.com"));
            message.To.Add(new MailboxAddress(mailMessage.ToName, mailMessage.ToAddress));
            message.Subject = mailMessage.Subject;
            message.Body = mailMessage.MessageText;
            
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("wahama.shop@gmail.com", "W24sQppF358Li");
                    client.Send(message);
                    client.Disconnect(true);
                    Console.WriteLine("Message has been sent.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
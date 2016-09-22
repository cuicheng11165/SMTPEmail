using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SMTPEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new SmtpServer { Host = "smtp.qq.com", Username = "cuicheng11165@qq.com", Password = "password,", Port = 25, Sender = "cuicheng11165@qq.com", UseSSL = true };
            var smtpClient = GetSmtpClient(server);
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;


            smtpClient.Send(server.Sender, "cuicheng11165@qq.com", "123", "222");

        }

        private static SmtpClient GetSmtpClient(SmtpServer setting)
        {

            var client = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = setting.Host,
                EnableSsl = setting.UseSSL,
                Timeout = 18000000,
            };

            if (setting.Port > 0 && setting.Port < 65536)
            {
                client.Port = setting.Port;
            }

            if (string.IsNullOrEmpty(setting.Username) || string.IsNullOrEmpty(setting.Password))
            {
                client.UseDefaultCredentials = true;
            }
            else
            {
                var credetntial = new NetworkCredential(setting.Username, setting.Password);
                client.UseDefaultCredentials = false;
                client.Credentials = credetntial;
            }

            return client;
        }
    }

    public class SmtpServer
    {

        public int Port { get; set; }

        public string Host { get; set; }

        public bool UseSSL { get; set; }

        public bool SecurePassword { get; set; }

        public string Sender { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}

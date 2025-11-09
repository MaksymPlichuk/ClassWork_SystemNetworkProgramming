using MailKit.Net.Imap;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;

namespace _12_NPMailKit.Library
{
    internal class ImapService
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public ImapService()
        {
        }
        public void ConnectAsync(string login, string pass)
        {
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate(login, pass);

            }
        }
    }
}

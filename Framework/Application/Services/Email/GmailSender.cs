using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application.Services.Email
{
    public class GmailSender : IEmailSender
    {
        private readonly string SenderTitle;
        private readonly string UserName;
        private readonly string Password;
        private readonly int Port;
        private readonly bool UseSSL;

        private readonly ILogger _Logger;
        public GmailSender(ILogger Logger)
        {
            _Logger = Logger;

            SenderTitle = "PrancaBeauty";
            UserName = "testdllearn@gmail.com";
            Password = "123456789Aabcdefg!@";
            Port = 587;
            UseSSL = true;
        }

        public bool Send(string _To, string _Subject, string _Message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(UserName, SenderTitle, Encoding.UTF8);
                mail.To.Add(new MailAddress(_To));
                mail.Subject = _Subject;
                mail.Body = _Message;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = Port;
                smtp.Credentials = new NetworkCredential(UserName, Password);
                smtp.EnableSsl = UseSSL;

                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public async Task SendAsync(string _To, string _Subject, string _Message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(UserName, SenderTitle, Encoding.UTF8);
                mail.To.Add(new MailAddress(_To));
                mail.Subject = _Subject;
                mail.Body = _Message;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = Port;
                smtp.Credentials = new NetworkCredential(UserName, Password);
                smtp.EnableSsl = UseSSL;

                smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                smtp.SendAsync(mail, _To);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string Token = (string)e.UserState;
                if (e.Cancelled)
                {

                }
                else if (e.Error != null)
                {
                    throw new Exception($"Token: [{Token}], Errors: [{e.Error.Message}]", e.Error);
                }
                else
                {
                    // Success
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }

    }
}

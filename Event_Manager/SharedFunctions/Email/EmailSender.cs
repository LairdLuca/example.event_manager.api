using System.Net.Mail;
using System.Net;

namespace Event_Manager.SharedFunctions.Email
{
    public abstract class EmailSender
    {
        protected string _emailAddressSender { get; set; }
        protected string _host { get; set; }
        protected int _port { get; set; }

        protected string _credentialEmail { get; set; }
        protected string _credentialPassword { get; set; }


        public Task SendEmail(string Subject, string email, string BodyHTML)
        {
            MailAddress mailAddressTo = new MailAddress(email);

            MailMessage mailMessage = new MailMessage(new MailAddress(_emailAddressSender), mailAddressTo);
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Subject;
            mailMessage.Body = BodyHTML;

            SmtpClient smtpClient = new SmtpClient(_host, _port);
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_credentialEmail, _credentialPassword);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Task.CompletedTask;
        }
    }


}

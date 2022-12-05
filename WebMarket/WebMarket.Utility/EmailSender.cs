using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;


namespace WebMarket.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromAddress = new MailAddress("arianmiramini1381@gmail.com", "WebMarket");
            var toAddress = new MailAddress(email);
            string fromPassword = "Ari@n1381";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                From = fromAddress,
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}

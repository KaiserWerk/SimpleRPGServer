using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using SimpleRPGServer.Models.Auth;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleRPGServer.Service
{
    public class EmailService : IEmailService
    {
        const string ROUTE_CONFIRM_REGISTRATION = "/api/registration/confirm";

        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();
        private readonly IConfiguration _config;

        private SmtpClient _client;

        public EmailService(IConfiguration configuration)
        {
            this._config = configuration;
            this._client = new SmtpClient(
                configuration.GetValue<string>("EmailSettings:Host"),
                configuration.GetValue<int>("EmailSettings:Port")
            );
            this._client.Credentials = new NetworkCredential(
                configuration.GetValue<string>("EmailSettings:Username"),
                configuration.GetValue<string>("EmailSettings:Password")
            );
            this._client.EnableSsl = configuration.GetValue<bool>("EmailSettings:UseSsl");
        }

        public async Task SendRegistrationConfirmationMail(RegistrationRequest req, string confirmationCode)
        {
            string confirmUrl = this._config.GetValue<string>("BaseUrl") + ROUTE_CONFIRM_REGISTRATION;
            string templateContent = GetMailTemplate("RegistrationConfirmation");
            templateContent = templateContent.Replace("[displayName]", req.DisplayName);
            templateContent = templateContent.Replace("[baseUrl]", confirmUrl);
            templateContent = templateContent.Replace("[confirmationCode]", confirmationCode);
            MailMessage mail = new MailMessage();
            mail.To.Add(req.Email);
            mail.From = new MailAddress(
                this._config.GetValue<string>("EmailSettings:FromAddr"),
                this._config.GetValue<string>("EmailSettings:FromName"), 
                System.Text.Encoding.UTF8
            );
            mail.Subject = "Confirm your account registration @ SimpleRPG Server";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = templateContent;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;            
            
            await this._client.SendMailAsync(mail); 
        }

        private string GetMailTemplate(string filename)
        {
            using var stream = this._assembly.GetManifestResourceStream($"{this._assembly.GetName().Name}.EmailTemplates.{filename}.html");
            using var r = new StreamReader(stream);
            return r.ReadToEnd();
        }

        ~EmailService()
        {
            this._client.Dispose();
        }
    }
}

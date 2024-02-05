using EcommerceShop.Application.Abstractions.IEmailService;
using EcommerceShop.Application.EmailSetting;
using EcommerceShop.Infrastructure.MailOptions;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Infrastructure.MailServices
{
    public class EmailService : IEmailService
    {
        private readonly MailJetOption options;

        public EmailService(IOptions<MailJetOption> options)
        {
            this.options = options.Value;
        }


        public async Task<bool> SendEmailAsync(MailSetting mailSetting)
        {
            MailjetClient mailjetClient = new MailjetClient(options.ApiKey, options.SecretKey);

            var email = new TransactionalEmailBuilder().
                WithFrom(new SendContact(options.FromEmail)).
                WithSubject(mailSetting.Subject).
                WithTo(new SendContact(mailSetting.To.FirstOrDefault())).
                WithHtmlPart(mailSetting.Body).
                Build();

            await mailjetClient.SendTransactionalEmailAsync(email);

            return true;
        }



        public async Task<bool> SendEmailsAsync(MailSetting mailSetting)
        {
            MailjetClient mailjetClient = new MailjetClient(options.ApiKey, options.SecretKey);
            var contacts = mailSetting.To.Select(_ => new SendContact(_));
            
            var email = new TransactionalEmailBuilder().
                WithFrom(new SendContact(options.FromEmail)).
                WithSubject(mailSetting.Subject).
                WithTo(contacts).
                WithHtmlPart(mailSetting.Body).
                Build();

            await mailjetClient.SendTransactionalEmailAsync(email);

            return true;
        }
    }
}

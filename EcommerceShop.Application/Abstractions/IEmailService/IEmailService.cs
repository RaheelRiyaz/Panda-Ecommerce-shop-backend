using EcommerceShop.Application.EmailSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IEmailService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(MailSetting mailSetting);
    }
}

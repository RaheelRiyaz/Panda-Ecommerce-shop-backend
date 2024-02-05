using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IEmailTemplateRenderer
{
    public interface IEmailTemplateRenderer
    {
        Task<string> RenderTemplateAsync(string templateName, object model);
    }
}

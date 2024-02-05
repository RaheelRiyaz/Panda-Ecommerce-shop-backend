using EcommerceShop.Application.Abstractions.IEmailService;
using EcommerceShop.Application.Abstractions.IEmailTemplateRenderer;
using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Infrastructure.MailOptions;
using EcommerceShop.Infrastructure.MailServices;
using EcommerceShop.Infrastructure.Services;
using EcommerceShop.Infrastructure.TemplateRenderer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Infrastructure
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJWTService>(new JWTService(configuration));

            services.Configure<MailJetOption>(configuration.GetSection("MailJetOptionSection"));

            services.AddScoped<IEmailTemplateRenderer, EmailTemplateRenderer>();

            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}

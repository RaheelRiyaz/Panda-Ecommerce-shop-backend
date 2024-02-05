using EcommerceShop.Application.Abstractions.IEmailTemplateRenderer;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Infrastructure.TemplateRenderer
{
    public class EmailTemplateRenderer : IEmailTemplateRenderer
    {
        public async Task<string> RenderTemplateAsync(string templateName, object model)
        {
            string template = string.Empty;

            try
            {
                var assemblyLocation = Assembly.GetExecutingAssembly().Location;
                string assemblyDirectory = Path.GetDirectoryName(assemblyLocation)!;

                var templateFolder = Path.Combine(assemblyDirectory, "EmailTemplates");

                var engine = new RazorLightEngineBuilder().
                    UseFileSystemProject(templateFolder).
                    UseMemoryCachingProvider().
                    EnableDebugMode().
                    Build();

                template = await engine.CompileRenderAsync(templateName, model);
            }
            catch (Exception exception){ throw new Exception(exception.Message); }

            return template;
        }
    }
}

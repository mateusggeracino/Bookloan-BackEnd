using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace MGG.Bookloan.WebAPI.Extensions
{
    public static class SwaggerConfig
    {
        public static void SwaggerAddServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Book Loan",
                        Version = "v1",
                        Description = "Book Loan API",
                        Contact = new Contact
                        {
                            Name = "MAteus G. Geracino",
                            Url = "https://github.com/mateusggeracino/Bookloan-BackEnd"
                        }
                    });

                var caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                var nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                var caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, "MGG.Bookloan.WebAPI.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        public static void SwaggerAddApp(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "BookLoanAPI - V1");
            });
        }
    }
}

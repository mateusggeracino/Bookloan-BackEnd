using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace MGG.Bookloan.WebAPI.Extensions
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerServices(this IServiceCollection services)
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
                            Name = "Mateus G. Geracino",
                            Url = "https://github.com/mateusggeracino/Bookloan-BackEnd"
                        }
                    });

                var pathApp =
                    PlatformServices.Default.Application.ApplicationBasePath;
                var nameApp =
                    PlatformServices.Default.Application.ApplicationName;
                var pathXmlDoc =
                    Path.Combine(pathApp, $"{nameApp}.xml");

                c.IncludeXmlComments(pathXmlDoc);
            });
        }

        public static void UseSwaggerApp(this IApplicationBuilder app)
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

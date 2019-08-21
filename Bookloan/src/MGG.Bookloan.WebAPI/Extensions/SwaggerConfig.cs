using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

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
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });
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

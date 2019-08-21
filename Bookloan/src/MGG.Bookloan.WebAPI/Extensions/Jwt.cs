using System.Text;
using MGG.Bookloan.Services.ViewModels.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MGG.Bookloan.WebAPI.Extensions
{
    public static class Jwt
    {
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT
            var jwtOptionsSection = configuration.GetSection("JwtOptions");
            services.Configure<JwtOptions>(jwtOptionsSection);

            var jwtOptions = jwtOptionsSection.Get<JwtOptions>();
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = true;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    //ValidateAudience = true,
                    //ValidAudience = jwtOptions.Valid,
                    ValidIssuer = jwtOptions.Issuer
                };
            });
        }
    }
}
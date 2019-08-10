using MGG.Bookloan.Services.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace MGG.Bookloan.WebAPI.Extensions
{
    public static class DependencyInjection
    {
        public static void DependencyInjectionRegister(this IServiceCollection services)
        {
            var mapper = MapperConfig.Register();
            services.AddSingleton(mapper.CreateMapper());
        }
    }
}
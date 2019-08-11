﻿using MGG.Bookloan.Business.Business;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Repository.Interfaces;
using MGG.Bookloan.Repository.Repository;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.Mapper;
using MGG.Bookloan.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MGG.Bookloan.WebAPI.Extensions
{
    public static class DependencyInjection
    {
        public static void DependencyInjectionRegister(this IServiceCollection services)
        {
            var mapper = MapperConfig.Register();
            services.AddSingleton(mapper.CreateMapper());

            Repository(services);
            Business(services);
            Services(services);
        }

        private static void Services(IServiceCollection services)
        {
            services.AddTransient<IClientServices, ClientServices>();
        }

        private static void Business(IServiceCollection services)
        {
            services.AddTransient<IClientBusiness, ClientBusiness>();
        }

        private static void Repository(IServiceCollection services)
        {
            services.AddTransient<IClientRepository, ClientRepository>();
        }
    }
}
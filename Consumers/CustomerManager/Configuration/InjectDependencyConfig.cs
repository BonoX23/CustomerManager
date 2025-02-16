using Application;
using Application.Ports;
using Data;
using Data.Repositories;
using Data.Repository;
using Domain.Contracts;
using Domain.Ports;

namespace CustomerManager.Configuration
{
    public static class InjectDependencyConfig
    {
        public static void InjectDependencyRegister(this IServiceCollection services)
        {
            services.AddScoped<CustomerDbContext>();


            #region AppServices

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAddressService, AddressService>();

            #endregion

            #region Repositories

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            #endregion
        }
    }
}

using CRUD_BAL.Interfaces;
using CRUD_BAL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CRUDAspNetCoreWebAPI.Extensions.Configuration
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();

            return services;
        }
    }
}

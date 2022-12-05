using Common.Filters;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Repositories;

namespace DemoApi.Extension
{
    public static class FilterExtension
    {
        public static IServiceCollection AddCustomFilter(this IServiceCollection services)
        {
            services.AddScoped<HyperAuthorizeFilter>();
            return services;
        }
    }
}
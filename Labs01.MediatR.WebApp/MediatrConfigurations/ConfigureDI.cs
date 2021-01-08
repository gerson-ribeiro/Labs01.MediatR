using Labs01.MediatR.Commons.Configurations.NotificationContext;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs01.MediatR.WebApp.MediatrConfigurations
{
    public static class ConfigureDI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection service)
        {
            service.AddScoped<NotificationContext>();

            return service;
        }
    }
}

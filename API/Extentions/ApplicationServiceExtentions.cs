using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //AddSingleton => expiry from the app start and when its stop
            //AddScoped => expiry from the http request and ends when the request is finished
            //AddTransient => the service is going to be created and destroyed as soon as the method is finished
            services.AddScoped<ITokenService, TokenService>();

            //we used lamda expression to pass an expression as a parameter
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using MoscowWeather.Core.Infrastrucrure;
using MoscowWeather.Core.Managers;
using MoscowWeather.Core.Service;
using MoscowWeather.Web.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 1;

        public void Register(IServiceCollection services)
        {
            services.AddScoped<WeatherManager>();
            services.AddScoped<Excel>();



            services.AddScoped<WeatherFactories>();
            
        }
    }
}

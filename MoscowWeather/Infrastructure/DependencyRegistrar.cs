using Microsoft.Extensions.DependencyInjection;
using MoscowWeather.Core.Infrastrucrure;
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
            throw new NotImplementedException();
        }
    }
}

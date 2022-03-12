using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoscowWeather.Core.Infrastrucrure
{
    public interface IDependencyRegistrar
    {
        void Register(IServiceCollection services);
        int Order { get; }
    }
}

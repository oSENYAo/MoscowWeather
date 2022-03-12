using MoscowWeather.Core.Infrastrucrure;
using MoscowWeather.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoscowWeather.Core.Managers
{
    public abstract class BaseManager
    {
        protected MoscowWeatherContext _context = null;
        public BaseManager(MoscowWeatherContext context)
        {
            _context = context;
        }
    }
}

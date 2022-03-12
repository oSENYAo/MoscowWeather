using Mithra.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoscowWeather.Core.Infrastrucrure
{
    public class EngineContext
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
            //create NopEngine as engine
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new WeatherEngine());
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MoscowWeather.Core.Infrastrucrure;
using MoscowWeather.Data;
using MoscowWeather.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoscowWeather.Core.Managers
{
    public class WeatherManager : BaseManager
    {
        public WeatherManager(MoscowWeatherContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Weather> GetWeatherByIdAsync(int id)
        {
            return await _context.Weathers.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Weather>> GetWeathersAsync()
        {
            return await _context.Weathers.ToListAsync();
        }
        public async Task AddWeatherAsync(Weather weather)
        {
            if (weather != null)
                 await _context.Weathers.AddAsync(weather);            
        }
        public async Task AddWeathersAsync(List<Weather> weathers)
        {
            await _context.Weathers.AddRangeAsync(weathers);
            await _context.SaveChangesAsync();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

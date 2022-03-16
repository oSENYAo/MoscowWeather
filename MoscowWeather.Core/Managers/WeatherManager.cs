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
        public async Task<List<Weather>> WeatherSearch(            
            int? startMonth = null,
            int? endMonth = null,
            int? startYear = null,
            int? endYear = null)
        {
            var weather = await _context.Weathers.OrderBy(x => x.Date)
                .Where(x => 
                       ((startMonth == null) || x.Date.Month >= startMonth)
                    && ((endMonth == null) || x.Date.Month <= endMonth)
                    && ((startYear == null) || x.Date.Year >= startYear)
                    && ((endYear == null) || x.Date.Year <= endYear))
                .ToListAsync();

            return weather;
        }
        public async Task<List<int>> GetAllMonthAsync()
        {
            return await _context.Weathers.Select(x => x.Date.Month).Distinct().OrderBy(x => ((uint)x)).ToListAsync();
        }
        public async Task<List<int>> GetAllYearsAsync()
        {
            return await _context.Weathers.Select(x => x.Date.Year).Distinct().OrderBy(x => ((uint)x)).ToListAsync();
        }
    }
}

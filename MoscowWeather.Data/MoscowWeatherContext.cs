using Microsoft.EntityFrameworkCore;
using MoscowWeather.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoscowWeather.Data
{
    public class MoscowWeatherContext : DbContext
    {
        public MoscowWeatherContext(DbContextOptions<MoscowWeatherContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Weather> Weathers { get; set; }
    }
}

using MoscowWeather.Web.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Models.Weather
{
    public class WeatherSearchModel : PaginationParams
    {
        public int Id { get; set; }
        public int? StartMonth { get; set; }
        public int? EndMonth { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }

    }
}

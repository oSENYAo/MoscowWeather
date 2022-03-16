using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Models.Weather
{
    public class WeatherListModel
    {
        public WeatherListModel()
        {
            FirstMonths = new List<SelectListItem>();
            FirstYears = new List<SelectListItem>();
            LastMonths = new List<SelectListItem>();
            LastYears = new List<SelectListItem>();
        }
        public List<SelectListItem> FirstMonths { get; set; }
        public List<SelectListItem> FirstYears { get; set; }
        public List<SelectListItem> LastMonths { get; set; }
        public List<SelectListItem> LastYears { get; set; }
        public int? FirstMonth { get; set; }
        public int? FirstYear { get; set; }
        public int? LastMonth { get; set; }
        public int? LastYear { get; set; }

    }
}

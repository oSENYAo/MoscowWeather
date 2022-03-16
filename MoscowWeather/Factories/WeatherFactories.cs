using Microsoft.AspNetCore.Mvc.Rendering;
using MoscowWeather.Core.Managers;
using MoscowWeather.Data.Domain;
using MoscowWeather.Web.Models.Pagination;
using MoscowWeather.Web.Models.Weather;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Factories
{
    public class WeatherFactories
    {
        private readonly WeatherManager _weatherManager;

        public WeatherFactories(WeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
        }
        private async Task<List<SelectListItem>> PrepareFirstMonthSelectList()
        {
            var months = await _weatherManager.GetAllMonthAsync();
            var dateInfo = new DateTimeFormatInfo();
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem("Выберите первый месяц", ""));
            for (int i = 0; i < months.Count; i++)
            {
                list.Add(new SelectListItem(dateInfo.GetMonthName(months[i]), months[i].ToString()));
            }
            return list;
        }
        private async Task<List<SelectListItem>> PrepareLastMonthSelectList()
        {
            var months = await _weatherManager.GetAllMonthAsync();
            var dateInfo = new DateTimeFormatInfo();
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem("Выберите последний месяц", ""));
            for (int i = 0; i < months.Count; i++)
            {
                list.Add(new SelectListItem(dateInfo.GetMonthName(months[i]), months[i].ToString()));
            }
            return list;
        }
        private async Task<List<SelectListItem>> PrepareFirstYearSelectList()
        {
            var years = await _weatherManager.GetAllYearsAsync();
            var dateInfo = new DateTimeFormatInfo();
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem("Выберите начальный год", ""));
            for (int i = 0; i < years.Count; i++)
            {
                list.Add(new SelectListItem((years[i]).ToString(), years[i].ToString()));
            }
            return list;
        }
        private async Task<List<SelectListItem>> PrepareLastYearSelectList()
        {
            var years = await _weatherManager.GetAllYearsAsync();
            var dateInfo = new DateTimeFormatInfo();
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem("Выберите конечный год", ""));
            for (int i = 0; i < years.Count; i++)
            {
                list.Add(new SelectListItem((years[i]).ToString(), years[i].ToString()));
            }
            return list;
        }

        public async Task<WeatherListModel> PrepareWeatherListModel()
        {
            var result = new WeatherListModel()
            {
                FirstMonths = await PrepareFirstMonthSelectList(),
                FirstYears = await PrepareFirstYearSelectList(),
                LastMonths = await PrepareLastMonthSelectList(),
                LastYears = await PrepareLastYearSelectList(),
            };
            return result;
        }
        public async Task<Tuple<List<WeatherModel>, PaginationMetaData>> PrepareWeatherSearchModel(
            int page,
            int itemPerPage,
            int? startMonth = null,
            int? endMonth = null,
            int? startYear = null,
            int? endYear = null)
        {
            var model = await _weatherManager.WeatherSearch(startMonth, endMonth, startYear, endYear);
            var paginationMetaData = new PaginationMetaData(model.Count(), page, itemPerPage);
            var s = model.Skip((page - 1) * itemPerPage)
            .Take(itemPerPage).ToList();
            var result = new List<WeatherModel>();
            foreach (var item in s)
            {
                result.Add(new WeatherModel
                {
                    Id = item.Id,
                    SpeedWind = item.SpeedWind,
                    AirTemperature = item.AirTemperature,
                    CloudBase = item.CloudBase,
                    cloudy = item.cloudy,
                    Date = item.Date.ToString("dd.MM.yyyy"),
                    Time = item.Date.ToString("HH:mm"),
                    DewPoint = item.DewPoint,
                    DirectionWind = item.DirectionWind,
                    HorizontalVisibility = item.HorizontalVisibility,
                    Pressure = item.Pressure,
                    Rel_humidity = item.Rel_humidity,
                    WeatherConditions = item.WeatherConditions
                });
            }
            
                
            

            return new Tuple<List<WeatherModel>, PaginationMetaData>(result, paginationMetaData);
        }
    }
}

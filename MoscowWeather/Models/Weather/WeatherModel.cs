using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Models.Weather
{
    public class WeatherModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public float? AirTemperature { get; set; }       // темп воздуха T
        public float? DewPoint { get; set; }             // точка росы Td
        public int? Pressure { get; set; }    // Атмосферное давление
        public string DirectionWind { get; set; }   // Направелние ветра
        public string SpeedWind { get; set; }   // Скорость ветра
        public int? CloudBase { get; set; }   // Нижняя граница видимости
        public int? HorizontalVisibility { get; set; }   // горизонтальная видимость
        public string WeatherConditions { get; set; }   // погодные явления
        public string Rel_humidity { get; set; }   // Отн. влажность воздуха проценты
        public string cloudy { get; set; }   // Облачность проценты
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MoscowWeather.Web.Extension.Error;
using MoscowWeather.Data;
using Microsoft.AspNetCore.Hosting;
using MoscowWeather.Core.Managers;
using MoscowWeather.Web.Factories;
using MoscowWeather.Core.Service;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.IO;
using System.Linq;
using MoscowWeather.Web.Models.Weather;
using MoscowWeather.Web.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace MoscowWeather.Web.Controllers
{
    public class WeatherController : Controller
    {
        private readonly MoscowWeatherContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly WeatherManager _weatherManager;
        private readonly WeatherFactories _weatherFactories;
        private readonly Excel _excel;
        private readonly INotyfService _notyfService;
        public WeatherController(MoscowWeatherContext context,
            IWebHostEnvironment appEnvironment,
            WeatherManager weatherManager,
            WeatherFactories weatherFactories,
            Excel excel,
            INotyfService notyfService)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _weatherManager = weatherManager;
            _weatherFactories = weatherFactories;
            _excel = excel;
            _notyfService = notyfService;
        }
        public IActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> FormFile)
        {
            var path = "";
            if (FormFile.Count <= 0)
            {
                _notyfService.Information("Please choose file(s)");
                return RedirectToAction("UploadFile");
            }
            try
            {
                foreach (var formFile in FormFile)
                {
                    path = "/Files/" + formFile.FileName;

                    //using (var stream = new FileStream(_appEnvironment.ContentRootPath + path, FileMode.Create))
                    //{
                    //    await formFile.CopyToAsync(stream);
                    //}
                    using (var stream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    if (!_excel.IsExcelFile(path))
                        throw new ExcelWrongTypeException("Wrong type fyle");

                    await _excel.WriteToDbAsync(_appEnvironment.WebRootPath + path);
                }
                _notyfService.Success("Download succes");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message + " " + path.Substring(7));
                _excel.Remove(_appEnvironment.WebRootPath + path);
            }
            return RedirectToAction("UploadFile");
        }
        public async Task<IActionResult> List()
        {
            var model =  await _weatherFactories.PrepareWeatherListModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult GetListWeathers([FromBody] WeatherSearchModel @params)
        {
            var result = _weatherFactories.PrepareWeatherSearchModel(@params.Page, @params.ItemPerPage,
                @params.StartMonth, @params.EndMonth, @params.StartYear, @params.EndYear).GetAwaiter().GetResult();
            
            return Ok(new 
            {
                Data = result.Item1,
                PagMetaData = result.Item2,
                Succes = true
            });
        }

    }
}

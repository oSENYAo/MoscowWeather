using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoscowWeather.Core.Managers;
using MoscowWeather.Core.Service;
using MoscowWeather.Data;
using MoscowWeather.Data.Domain;
using MoscowWeather.Web.Extension.Error;
using MoscowWeather.Web.Factories;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoscowWeather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MoscowWeatherContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly WeatherManager _weatherManager;
        private readonly WeatherFactories _weatherFactories;
        private readonly Excel _excel;
        private readonly INotyfService _notyfService;

        public HomeController(
            ILogger<HomeController> logger,
            MoscowWeatherContext context,
            IWebHostEnvironment appEnvironment,
            WeatherManager weatherManager,
            WeatherFactories weatherFactories,
            Excel excel,
            INotyfService notyfService)
        {
            _logger = logger;
            _context = context;
            _appEnvironment = appEnvironment;
            _weatherManager = weatherManager;
            _weatherFactories = weatherFactories;
            _excel = excel;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View(_context.Weathers.ToList());
        }
        
        public IActionResult Test()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Test(List<IFormFile> FormFile)
        {
            var path = "";
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
            return RedirectToAction("Test");
        }
    }
}

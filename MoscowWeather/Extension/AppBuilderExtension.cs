using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MoscowWeather.Core.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Extension
{
    public static class AppBuilderExtension
    {
        public static void UseNopExceptionHandler(this IApplicationBuilder application)
        {
            #region text
            application.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    //var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    //if (exception == null)
                    //    return;
                    //Message = "The process cannot access the file 'D:\\repositivs\\MoscowWeather\\MoscowWeather\\wwwroot\\Files\\log.txt' because it is being used by another process."
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    await context.Response.WriteAsJsonAsync(new { error = exception.Message });
                    //try
                    //{
                    //    //get current customer
                    //    //var userManager = EngineContext.Current.Resolve<MithraUserManager>();

                    //    //log error
                    //    //await EngineContext.Current.Resolve<Logger>().ErrorAsync(exception.Message, exception, await userManager.GetCurrentUser());
                    //}
                    //finally
                    //{
                    //    //rethrow the exception to show the error page
                    //    //throw exception;
                    //    ExceptionDispatchInfo.Throw(exception);
                    //}
                });
            });
            #endregion
        }
    }
}

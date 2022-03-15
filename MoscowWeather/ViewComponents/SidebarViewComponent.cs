using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MoscowWeather.Web.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

    }
}

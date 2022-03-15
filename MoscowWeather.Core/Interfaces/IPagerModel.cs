using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoscowWeather.Core.Interfaces
{
    public interface IPagerModel
    {
        int PageSize { get; set; }
        int CurrentPage { get; set; }
        int TotalPages { get; set; }
        string OrderBy { get; set; }
        bool Ascending { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Models.Pagination
{
    public class CursorParam
    {
        public int Count { get; set; } = 50;
        public int Cursor { get; set; } = 1;
        public bool HasPreviosly { get; set; } = false;
        public bool HasNext { get; set; } = false;
    }
}

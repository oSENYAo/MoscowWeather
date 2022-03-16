using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Extension.Error
{
    public class ExcelWrongTypeException : Exception
    {
        public ExcelWrongTypeException()
        {
        }

        public ExcelWrongTypeException(string message)
            : base(message)
        {
        }

        public ExcelWrongTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

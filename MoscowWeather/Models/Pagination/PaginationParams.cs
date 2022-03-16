using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowWeather.Web.Models.Pagination
{
    public class PaginationParams
    {
        private const int _maxItemPerPage = 50;
        private int itemPerPage;
        public int Page { get; set; } = 1;
        public int ItemPerPage 
        { 
            get 
            {
                return itemPerPage;
            } 
            set 
            {
                itemPerPage = value > _maxItemPerPage ? _maxItemPerPage : value;
            } 
        }
    }
}

using Business_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_layer.Filter
{
    public class CursusFilter : IFilter
    {
        public CursusFilter()
        {
            Type = "";
            Direction = "asc";
            Titel = "";
            SortBy = "";
            PageSize = 16;
            Page = 0;
        }
        public string Type { get; set; }
        public string Direction { get; set; }
        public string Titel { get; set; }
        public string SortBy { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }

    }
}

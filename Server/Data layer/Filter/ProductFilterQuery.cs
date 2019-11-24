
using Data_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_layer.Filter
{
    public abstract class ProductFilter
    {
        public string Type { get; set; } = "";
        public string Direction { get; set; } = "asc";
        public string Titel { get; set; } = "";
        public string SortBy { get; set; } = "";
        public int PageSize { get; set; } = 16;
        public int Page { get; set; } = 0;
    }
}

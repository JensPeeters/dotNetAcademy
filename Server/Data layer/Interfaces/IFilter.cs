using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Interfaces
{
    public interface IFilter
    {
        string Type { get; set; }
        string Direction { get; set; }
        string Titel { get; set; }
        string SortBy { get; set; }
        int PageSize { get; set; }
        int Page { get; set; }
    }
}

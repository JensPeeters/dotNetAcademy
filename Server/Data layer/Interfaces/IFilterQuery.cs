using System;
using System.Collections.Generic;
using System.Text;

namespace Data_layer.Interfaces
{
    public interface IFilterQuery
    {
        string Type { get; set; }
        string Direction { get; set; }
        string Titel { get; set; }
        string SortBy { get; set; }
        int PageSize { get; set; }
        int Page { get; set; }
    }
}

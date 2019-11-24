
using Data_layer.Filter;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_layer.Interfaces
{
    public interface IContextFilter
    {
        IQueryable<Product> Filter(ProductFilter filter, IQueryable<Product> query);
    }
}

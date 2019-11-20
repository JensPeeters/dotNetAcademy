using Business_layer.Interfaces;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_layer.Interfaces
{
    public interface ISortFilter
    {
        IQueryable<Product> Filter(IFilter filter, IQueryable<Product> query);
    }
}

﻿using Data_layer.Interfaces;
using Data_layer.Model;
using System.Linq;

namespace Data_layer.Filter
{
    public class ContextFilter : IContextFilter
    {
        public IQueryable<Product> Filter(ProductFilter filter, IQueryable<Product> query)
        {
            if (!string.IsNullOrEmpty(filter.Type))
                query = query.Where(b => b.Type.ToLower().Contains(filter.Type.ToLower().Trim()));

            if (!string.IsNullOrEmpty(filter.Titel))
                query = query.Where(b => b.Titel.ToLower().Contains(filter.Titel.ToLower().Trim()));

            if (string.IsNullOrEmpty(filter.SortBy))
                filter.SortBy = "ordernumber";

            switch (filter.SortBy.ToLower())
            {
                case "ordernumber":
                    if (filter.Direction == "asc")
                        query = query.OrderBy(b => b.OrderNumber);
                    else if (filter.Direction == "desc")
                        query = query.OrderByDescending(b => b.OrderNumber);
                    break;
                case "id":
                    if (filter.Direction == "asc")
                        query = query.OrderBy(b => b.ID);
                    else if (filter.Direction == "desc")
                        query = query.OrderByDescending(b => b.ID);
                    break;
                case "prijs":
                    if (filter.Direction == "asc")
                        query = query.OrderBy(b => b.Prijs);
                    else if (filter.Direction == "desc")
                        query = query.OrderByDescending(b => b.Prijs);
                    break;
                case "titel":
                    if (filter.Direction == "asc")
                        query = query.OrderBy(b => b.Titel);
                    else if (filter.Direction == "desc")
                        query = query.OrderByDescending(b => b.Titel);
                    break;
                case "type":
                    if (filter.Direction == "asc")
                        query = query.OrderBy(b => b.Type);
                    else if (filter.Direction == "desc")
                        query = query.OrderByDescending(b => b.Type);
                    break;
                default:
                    if (filter.Direction == "asc")
                        query = query.OrderBy(b => b.ID);
                    else if (filter.Direction == "desc")
                        query = query.OrderByDescending(b => b.ID);
                    break;
            }
            query = query.Skip(filter.Page * filter.PageSize);
            query = query.Take(filter.PageSize);

            return query;
        }
    }
}

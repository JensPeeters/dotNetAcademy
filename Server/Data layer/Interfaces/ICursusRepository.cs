using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_layer.Interfaces
{
    interface ICursusRepository
    {
        List<Product> GetProducts();
        Product GetProductByTitel(string titel);
        Product GetProductById(int id);
        Product AddProduct(Product product);
        Product DeleteProduct(int id);
        Product UpdateProduct(Product product);
    }
}

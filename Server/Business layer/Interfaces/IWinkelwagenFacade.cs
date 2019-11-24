using Business_layer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Interfaces
{
    public interface IWinkelwagenFacade
    {
        WinkelwagenDTO GetBagForCustomer(string custId);
        WinkelwagenDTO AddProduct(string userId, int prodId, int count, string type);
        WinkelwagenDTO UpdateProductAantal(string userId, int prodId, int count);
        WinkelwagenDTO DeleteProduct(string userId, int prodId);
    }
}

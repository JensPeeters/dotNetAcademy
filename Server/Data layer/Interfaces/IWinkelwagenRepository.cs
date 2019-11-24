using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_layer.Interfaces
{
    public interface IWinkelwagenRepository
    {
        Winkelwagen GetWinkelwagenByKlantId(string custId);
        Winkelwagen AddProduct(string userId, int prodId, int count, string type);
        Winkelwagen DeleteProduct(string userId, int prodId);
        Winkelwagen UpdateProduct(string userId, int prodId, int count);
    }
}

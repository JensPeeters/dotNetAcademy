using Data_layer.Model;

namespace Data_layer.Interfaces
{
    public interface IWinkelwagenRepository
    {
        Winkelwagen GetWinkelwagenByKlantId(string custId);
        Winkelwagen AddProduct(string userId, int prodId, int count, string type);
        void SaveChanges();
        Winkelwagen DeleteProduct(string userId, int prodId);
        Winkelwagen UpdateProduct(string userId, int prodId, int count);
        Winkelwagen CreateWinkelwagen(Klant klant);
    }
}

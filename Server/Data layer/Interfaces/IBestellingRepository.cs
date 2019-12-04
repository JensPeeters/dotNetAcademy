using Data_layer.Model;
using System.Collections.Generic;

namespace Data_layer.Interfaces
{
    public interface IBestellingRepository
    {
        List<Bestelling> GetBestellingenByCustomerId(string custId);
        Bestelling AddBestellingToCustomer(Bestelling bestelling);
        void SaveChanges();
    }
}

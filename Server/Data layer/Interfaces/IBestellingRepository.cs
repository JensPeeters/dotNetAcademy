using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_layer.Interfaces
{
    public interface IBestellingRepository
    {
        List<Bestelling> GetBestellingenByCustomerId(string custId);
    }
}

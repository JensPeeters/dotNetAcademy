using Business_layer.DTO;
using Business_layer.Interfaces.Mapping;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Mapping
{
    public class WinkelwagenMapper : IWinkelwagenMapper
    {
        public WinkelwagenDTO MapToDTO(Winkelwagen model)
        {
            return new WinkelwagenDTO()
            {
                Datum = model.Datum,
                Id = model.Id,
                Klant = model.Klant,
                Producten = model.Producten,
                TotaalPrijs = model.TotaalPrijs
            };
        }
        public Winkelwagen MapToModel(WinkelwagenDTO dto)
        {
            return new Winkelwagen()
            {
                Datum = dto.Datum,
                Id = dto.Id,
                Klant = dto.Klant,
                Producten = dto.Producten,
                TotaalPrijs = dto.TotaalPrijs
            };
        }
    }
}

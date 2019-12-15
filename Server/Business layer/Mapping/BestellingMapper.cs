using Business_layer.DTO;
using Business_layer.Interfaces.Mapping;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Mapping
{
    public class BestellingMapper : IBestellingMapper
    {
        public BestellingDTO MapToDTO(Bestelling model)
        {
            return new BestellingDTO()
            {
                Id = model.Id,
                Datum = model.Datum,
                Klant = model.Klant,
                Producten = model.Producten,
                TotaalPrijs = model.TotaalPrijs
            };
        }

        public Bestelling MapToModel(BestellingCreateUpdateDTO dto)
        {
            return new Bestelling()
            {
                Datum = dto.Datum,
                Klant = dto.Klant,
                Producten = dto.Producten,
                TotaalPrijs = dto.TotaalPrijs
            };
        }
    }
}

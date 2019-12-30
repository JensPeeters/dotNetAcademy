using Business_layer.DTO;
using Business_layer.Interfaces.Mapping;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Mapping
{
    public class TrajectMapper : ITrajectMapper
    {
        public TrajectDTO MapToDTO(Traject model)
        {
            return new TrajectDTO()
            {
                Beschrijving = model.Beschrijving,
                Categorie = model.Categorie,
                FotoURLCard = model.FotoURLCard,
                ID = model.ID,
                LangeBeschrijving = model.LangeBeschrijving,
                IsBuyable = model.IsBuyable,
                Prijs = model.Prijs,
                Titel = model.Titel,
                Type = model.Type,
                Cursussen = model.Cursussen,
                OrderNumber = model.OrderNumber
            };
        }

        public Traject MapToModel(TrajectCreateUpdateDTO dto)
        {
            return new Traject()
            {
                Beschrijving = dto.Beschrijving,
                Categorie = dto.Categorie,
                FotoURLCard = dto.FotoURLCard,
                LangeBeschrijving = dto.LangeBeschrijving,
                IsBuyable = dto.IsBuyable,
                Prijs = dto.Prijs,
                Titel = dto.Titel,
                Cursussen = dto.Cursussen,
                Type = dto.Type,
                OrderNumber = dto.OrderNumber
            };
        }
    }
}

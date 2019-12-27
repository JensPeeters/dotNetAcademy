using Business_layer.DTO;
using Business_layer.Interfaces.Mapping;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Mapping
{
    public class CursusMapper : ICursusMapper
    {
        public CursusDTO MapToDTO(Cursus model)
        {
            return new CursusDTO()
            {
                Beschrijving = model.Beschrijving,
                Categorie = model.Categorie,
                FotoURLCard = model.FotoURLCard,
                ID = model.ID,
                IsBuyable = model.IsBuyable,
                LangeBeschrijving = model.LangeBeschrijving,
                Prijs = model.Prijs,
                Titel = model.Titel,
                Type = model.Type,
                OrderNumber = model.OrderNumber
            };
        }

        public Cursus MapToModel(CursusCreateUpdateDTO dto)
        {
            return new Cursus()
            {
                Beschrijving = dto.Beschrijving,
                Categorie = dto.Categorie,
                FotoURLCard = dto.FotoURLCard,
                LangeBeschrijving = dto.LangeBeschrijving,
                IsBuyable = dto.IsBuyable,
                Prijs = dto.Prijs,
                Titel = dto.Titel,
                Type = dto.Type,
                OrderNumber = dto.OrderNumber
            };
        }
    }
}

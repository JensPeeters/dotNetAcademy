using Business_layer.DTO;
using Business_layer.Interfaces.Mapping;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Mapping
{
    public class KlantMapper : IKlantMapper
    {
        public KlantDTO MapToDTO(Klant model)
        {
            return new KlantDTO()
            {
                AzureId = model.AzureId,
                Bestellingen = model.Bestellingen,
                Winkelwagens = model.Winkelwagens
            };
        }

        public Klant MapToModel(string klantId)
        {
            return new Klant()
            {
                AzureId = klantId
            };
        }
    }
}

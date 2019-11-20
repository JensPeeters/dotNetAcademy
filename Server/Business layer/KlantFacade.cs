using Business_layer.DTO;
using Data_layer.Model;
using Data_layer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer
{
    public class KlantFacade
    {
        private readonly KlantRepository repository;

        public KlantFacade(KlantRepository repository)
        {
            this.repository = repository;
        }

        private static Klant ConvertCreateUpdateDTOToKlant(string klantId)
        {
            return new Klant()
            {
                AzureId = klantId
            };
        }
        private static KlantDTO ConvertKlantToDTO(Klant klant)
        {
            return new KlantDTO()
            {
                AzureId = klant.AzureId,
                Bestellingen = klant.Bestellingen,
                Winkelwagens = klant.Winkelwagens
            };
        }

        public KlantDTO CreateKlant(string klantId)
        {
            var klant = repository.GetKlantByID(klantId);
            if (klant != null)
                return null;
            var newKlant = ConvertCreateUpdateDTOToKlant(klantId);
            var createdKlant = repository.CreateKlant(newKlant);
            return ConvertKlantToDTO(createdKlant);
        }

        public KlantDTO GetKlant(string klantId)
        {
            var klant = repository.GetKlantByID(klantId);
            if (klant == null)
                return null;
            return ConvertKlantToDTO(klant);
        }
    }
}

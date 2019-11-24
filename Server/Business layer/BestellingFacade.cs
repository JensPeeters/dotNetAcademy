using Business_layer.DTO;
using Business_layer.Interfaces;
using Data_layer;
using Data_layer.Interfaces;
using Data_layer.Model;
using Data_layer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_layer
{
    public class BestellingFacade : IBestellingFacade
    {
        private readonly IBestellingRepository _repository;

        public BestellingFacade(IBestellingRepository repository)
        {
            this._repository = repository;
        }
        public List<BestellingDTO> GetBestellingen(string custId)
        {
            var bestellingen = new List<BestellingDTO>();
            foreach (var bestelling in _repository.GetBestellingen(custId))
            {
                bestellingen.Add(
                    new BestellingDTO()
                    {
                        Datum = bestelling.Datum,
                        Id = bestelling.Id,
                        Klant = bestelling.Klant,
                        Producten = bestelling.Producten,
                        TotaalPrijs = bestelling.TotaalPrijs
                    });
            }
            return bestellingen;
        }
    }
}

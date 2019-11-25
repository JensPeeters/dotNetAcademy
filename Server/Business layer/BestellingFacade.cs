using Business_layer.DTO;
using Business_layer.Interfaces;
using Data_layer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Business_layer
{
    public class BestellingFacade : IBestellingFacade
    {
        private readonly IBestellingRepository _repository;

        public BestellingFacade(IBestellingRepository repository)
        {
            this._repository = repository;
        }
        public List<BestellingDTO> GetBestellingenByCustomerId(string custId)
        {
            return _repository.GetBestellingenByCustomerId(custId)
                         .Select(bestelling => new BestellingDTO()
                         {
                             Datum = bestelling.Datum,
                             Id = bestelling.Id,
                             Klant = bestelling.Klant,
                             Producten = bestelling.Producten,
                             TotaalPrijs = bestelling.TotaalPrijs
                         }).ToList();
        }
    }
}

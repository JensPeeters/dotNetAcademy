using Business_layer.DTO;
using Business_layer.Interfaces;
using Business_layer.Interfaces.Mapping;
using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business_layer
{
    public class BestellingFacade : IBestellingFacade
    {
        private readonly IBestellingRepository _repositoryBestelling;
        private readonly IWinkelwagenRepository _repositoryWinkelwagen;
        private readonly IKlantRepository _klantRepository;
        private readonly ICostCalculator _costCalculator;
        private readonly IBestellingMapper _bestellingMapper;

        public BestellingFacade(IBestellingRepository repositoryBestelling,
            IWinkelwagenRepository repositoryWinkelwagen,
            IKlantRepository klantRepository,
            ICostCalculator costCalculator,
            IBestellingMapper bestellingMapper)
        {
            _repositoryBestelling = repositoryBestelling;
            _repositoryWinkelwagen = repositoryWinkelwagen;
            _klantRepository = klantRepository;
            _costCalculator = costCalculator;
            _bestellingMapper = bestellingMapper;
        }
        public List<BestellingDTO> GetBestellingenByCustomerId(string custId)
        {
            return _repositoryBestelling.GetBestellingenByCustomerId(custId)
                         .Select(bestelling => new BestellingDTO()
                         {
                             Datum = bestelling.Datum,
                             Id = bestelling.Id,
                             Klant = bestelling.Klant,
                             Producten = bestelling.Producten,
                             TotaalPrijs = bestelling.TotaalPrijs
                         }).ToList();
        }

        public BestellingDTO GetBestellingById(int bestellingId)
        {
            var bestelling = _repositoryBestelling.GetBestellingById(bestellingId);
            if (bestelling == null)
                return null;
            return _bestellingMapper.MapToDTO(bestelling);
        }

        public BestellingDTO AddBestellingToCustomer(string custId)
        {
            var bestelling = new BestellingCreateUpdateDTO
            {
                Producten = new List<BestellingItem>(),
                Datum = DateTime.Now,
                Klant = _klantRepository.GetKlantByID(custId)
            };
            var winkelwagenGebruiker = _repositoryWinkelwagen.GetWinkelwagenByKlantId(custId);
            foreach (VerkoopItem item in winkelwagenGebruiker.Producten)
            {
                bestelling.Producten.Add(new BestellingItem() { Aantal = item.Aantal,Product = item.Product });
            }
            bestelling.TotaalPrijs = _costCalculator.CalculateCost(winkelwagenGebruiker);
            var newBestelling = _bestellingMapper.MapToModel(bestelling);
            var createdBestelling = _repositoryBestelling.AddBestellingToCustomer(newBestelling);

            winkelwagenGebruiker.TotaalPrijs = 0.0;
            winkelwagenGebruiker.Producten = new List<WinkelwagenItem>();

            try
            {
                _repositoryBestelling.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _bestellingMapper.MapToDTO(createdBestelling);
        }
    }
}

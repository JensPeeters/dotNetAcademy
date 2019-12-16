using Business_layer.DTO;
using Business_layer.Interfaces;
using Business_layer.Interfaces.Mapping;
using Data_layer.Interfaces;
using Data_layer.Model;
using System;
using System.Collections.Generic;

namespace Business_layer
{
    public class WinkelwagenFacade : IWinkelwagenFacade
    {
        private readonly ICostCalculator _calculator;
        private readonly IWinkelwagenRepository _repositoryWinkelwagen;
        private readonly IKlantRepository _repositoryKlant;
        private readonly IWinkelwagenMapper _winkelwagenMapper;

        public WinkelwagenFacade(ICostCalculator calculator,
            IWinkelwagenRepository repositoryWinkelwagen,
            IKlantRepository repositoryKlant,
            IWinkelwagenMapper winkelwagenMapper)
        {
            _calculator = calculator;
            _repositoryWinkelwagen = repositoryWinkelwagen;
            _repositoryKlant = repositoryKlant;
            _winkelwagenMapper = winkelwagenMapper;
        }

        /// <summary>
        /// Haal de shopping mandje op, indien nog niet bestaat wordt 1 aangemaakt.
        /// Indien het wel bestaat, wordt het meest recente genomen voor de betreffende klant.
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public WinkelwagenDTO GetBagForCustomer(string custId)
        {
            var klant = _repositoryKlant.GetKlantByID(custId);
            var isCraeted = false;
            if (klant == null)
            {
                _repositoryKlant.CreateKlant(new Klant()
                {
                    AzureId = custId,
                    Winkelwagens = new List<Winkelwagen>()
                });
                isCraeted = true;
            }
            if (klant.Winkelwagens == null || klant.Winkelwagens.Count == 0)
            {
                _repositoryWinkelwagen.CreateWinkelwagen(klant);
                isCraeted = true;
            }
            var winkelwagen = _repositoryWinkelwagen.GetWinkelwagenByKlantId(custId);
            try
            {
                if (isCraeted == true)
                    _repositoryWinkelwagen.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _winkelwagenMapper.MapToDTO(winkelwagen);
        }

        /// <summary>
        /// Voeg een product toe in een mandje en bereken de totaalprijs.
        /// </summary>
        /// <param name="userId">ID van de gebruiker</param>
        /// <param name="prodId">ID van het product</param>
        /// <param name="count">Aantal exemplaren van het betreffende product</param>
        /// <param name="type">Soort type van product</param>
        /// <returns></returns>
        public WinkelwagenDTO AddProduct(string userId, int prodId, int count,string type)
        {
            CheckIfKlantExists(userId);
            var winkelwagen = _repositoryWinkelwagen.AddProduct(userId, prodId, count, type);
            //Herberekenen van de totaal prijs
            winkelwagen.TotaalPrijs = _calculator.CalculateCost(winkelwagen);
            try
            {
                _repositoryWinkelwagen.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _winkelwagenMapper.MapToDTO(winkelwagen);
        }

        /// <summary>
        /// Voeg een product toe in een mandje en bereken de totaalprijs.
        /// </summary>
        /// <param name="userId">ID van de gebruiker</param>
        /// <param name="prodId">ID van het product</param>
        /// <param name="count">Aantal exemplaren van het betreffende product</param>
        /// <returns></returns>
        public WinkelwagenDTO UpdateProductAantal(string userId, int prodId, int count)
        {
            CheckIfKlantExists(userId);
            var winkelwagen = _repositoryWinkelwagen.UpdateProduct(userId, prodId, count);
            //Herberekenen van de totaal prijs
            winkelwagen.TotaalPrijs = _calculator.CalculateCost(winkelwagen);
            try
            {
                _repositoryWinkelwagen.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _winkelwagenMapper.MapToDTO(winkelwagen);
        }

        /// <summary>
        /// Voeg een product toe in een mandje en bereken de totaalprijs.
        /// </summary>
        /// <param name="userId">ID van de gebruiker</param>
        /// <param name="prodId">ID van het product</param>
        /// <returns></returns>
        public WinkelwagenDTO DeleteProduct(string userId, int prodId)
        {
            CheckIfKlantExists(userId);
            var winkelwagen = _repositoryWinkelwagen.DeleteProduct(userId, prodId);

            //Herberekenen van de totaal prijs
            winkelwagen.TotaalPrijs = _calculator.CalculateCost(winkelwagen);
            try
            {
                _repositoryWinkelwagen.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _winkelwagenMapper.MapToDTO(winkelwagen);
        }

        private void CheckIfKlantExists(string userId)
        {
            var klant = _repositoryKlant.GetKlantByID(userId);
            if (klant == null)
            {
                _repositoryKlant.CreateKlant(new Klant()
                {
                    AzureId = userId,
                    Winkelwagens = new List<Winkelwagen>()
                });
            }
            if (klant.Winkelwagens == null || klant.Winkelwagens.Count == 0)
            {
                _repositoryWinkelwagen.CreateWinkelwagen(klant);
            }
        }

        
    }
}

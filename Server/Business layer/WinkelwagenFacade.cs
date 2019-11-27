using Business_layer.DTO;
using Business_layer.Interfaces;
using Data_layer.Interfaces;
using Data_layer.Model;
using System;

namespace Business_layer
{
    public class WinkelwagenFacade : IWinkelwagenFacade
    {
        private readonly ICostCalculator calculator;
        private readonly IWinkelwagenRepository _repositoryWinkelwagen;

        public WinkelwagenFacade(ICostCalculator calculator,
            IWinkelwagenRepository repositoryWinkelwagen)
        {
            this.calculator = calculator;
            this._repositoryWinkelwagen = repositoryWinkelwagen;
        }

        /// <summary>
        /// Haal de shopping mandje op, indien nog niet bestaat wordt 1 aangemaakt.
        /// Indien het wel bestaat, wordt het meest recente genomen voor de betreffende klant.
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public WinkelwagenDTO GetBagForCustomer(string custId)
        {
            var winkelwagen = _repositoryWinkelwagen.GetWinkelwagenByKlantId(custId);
            try
            {
                _repositoryWinkelwagen.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertWinkelwagenToDTO(winkelwagen);
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
            var winkelwagen = _repositoryWinkelwagen.AddProduct(userId, prodId, count, type);
            //Herberekenen van de totaal prijs
            winkelwagen.TotaalPrijs = calculator.CalculateCost(winkelwagen);
            try
            {
                _repositoryWinkelwagen.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertWinkelwagenToDTO(winkelwagen);
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
            var winkelwagen = _repositoryWinkelwagen.UpdateProduct(userId, prodId, count);
            //Herberekenen van de totaal prijs
            winkelwagen.TotaalPrijs = calculator.CalculateCost(winkelwagen);
            try
            {
                _repositoryWinkelwagen.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertWinkelwagenToDTO(winkelwagen);
        }

        /// <summary>
        /// Voeg een product toe in een mandje en bereken de totaalprijs.
        /// </summary>
        /// <param name="userId">ID van de gebruiker</param>
        /// <param name="prodId">ID van het product</param>
        /// <returns></returns>
        public WinkelwagenDTO DeleteProduct(string userId, int prodId)
        {
            var winkelwagen = _repositoryWinkelwagen.DeleteProduct(userId, prodId);

            //Herberekenen van de totaal prijs
            winkelwagen.TotaalPrijs = calculator.CalculateCost(winkelwagen);
            try
            {
                _repositoryWinkelwagen.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertWinkelwagenToDTO(winkelwagen);
        }

        private static WinkelwagenDTO ConvertWinkelwagenToDTO(Winkelwagen winkelwagen)
        {
            return new WinkelwagenDTO()
            {
                Datum = winkelwagen.Datum,
                Id = winkelwagen.Id,
                Klant = winkelwagen.Klant,
                Producten = winkelwagen.Producten,
                TotaalPrijs = winkelwagen.TotaalPrijs
            };
        }

        
    }
}

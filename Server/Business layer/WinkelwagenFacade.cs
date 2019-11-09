using Business_layer.Interfaces;
using Data_layer;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_layer
{
    public class WinkelwagenFacade
    {
        private readonly DatabaseContext context;
        private readonly ICostCalculator calculator;

        public WinkelwagenFacade(DatabaseContext context, ICostCalculator calculator)
        {
            this.context = context;
            this.calculator = calculator;
        }


        public Winkelwagen GetWinkelwagen(int id)
        {
            return context.Winkelwagens.Include(d => d.Producten)
                                        .ThenInclude(i => i.Product)
                                        .SingleOrDefault(d => d.Id == id);
        }

        /// <summary>
        /// Haal de shopping mandje op, indien nog niet bestaat wordt 1 aangemaakt.
        /// Indien het wel bestaat, wordt het meest recente genomen voor de betreffende klant.
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public Winkelwagen GetBagForCustomer(string custId)
        {
            var klant = context.Klanten
                .Include(d => d.Winkelwagens)
                .ThenInclude(b => b.Producten)
                .ThenInclude(i => i.Product)
                .ThenInclude(d => (d as Traject).Cursussen)
                .SingleOrDefault(d => d.AzureId == custId);

            if (klant == null)
            {
                klant = new Klant()
                {
                    AzureId = custId,
                    Winkelwagens = new List<Winkelwagen>()
                };
                context.Klanten.Add(klant);
                //SaveContext();
            }

            if (klant.Winkelwagens == null || klant.Winkelwagens.Count == 0)
            {
                var winkelwagen = new Winkelwagen()
                {
                    Datum = DateTime.Now,
                    Producten = new List<WinkelwagenItem>()

                };
                klant.Winkelwagens.Add(winkelwagen);
                SaveContext();
            }
            return klant.Winkelwagens
                .OrderByDescending(d => d.Datum)
                .FirstOrDefault();
        }

        private void SaveContext()
        {
            context.SaveChanges();
        }


        /// <summary>
        /// Voeg een product toe in een mandje en bereken de totaalprijs.
        /// </summary>
        /// <param name="userId">ID van de gebruiker</param>
        /// <param name="prodId">ID van het product</param>
        /// <param name="count">Aantal exemplaren van het betreffende product</param>
        /// <param name="type">Soort type van product</param>
        /// <returns></returns>
        public Winkelwagen AddProduct(string userId, int prodId, int count,string type)
        {
            var winkelwagen = context.Winkelwagens
                .Include(a => a.Producten)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(d => d.Klant.AzureId == userId);
            winkelwagen = CheckIfWinkelwagenExists(userId, winkelwagen);

            try
            {
                foreach (var product in winkelwagen.Producten)
                {
                    if (product.Product.ID == prodId)
                    {
                        product.Aantal += count;
                        return winkelwagen;
                    }
                }
                //not found, create new:
                var product2 = new WinkelwagenItem();
                if (type == "Traject")
                {
                    product2.Product = context.Trajecten.Find(prodId);
                    product2.Aantal = count;
                }
                else if (type == "Cursus")
                {
                    product2.Product = context.Cursussen.Find(prodId);
                    product2.Aantal = count;
                }
                winkelwagen.Producten.Add(product2);
                return winkelwagen;
            }
            finally
            {
                //Herberekenen van de totaal prijs
                winkelwagen.TotaalPrijs = calculator.CalculateCost(winkelwagen);
                SaveContext();
            }
        }

        /// <summary>
        /// Voeg een product toe in een mandje en bereken de totaalprijs.
        /// </summary>
        /// <param name="userId">ID van de gebruiker</param>
        /// <param name="prodId">ID van het product</param>
        /// <returns></returns>
        public Winkelwagen DeleteProduct(string userId, int prodId)
        {
            var winkelwagen = context.Winkelwagens
                .Include(a => a.Producten)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(d => d.Klant.AzureId == userId);
            winkelwagen = CheckIfWinkelwagenExists(userId, winkelwagen);
            try
            {
                var winkelwagenItem = winkelwagen.Producten.FirstOrDefault(a => a.Id == prodId);
                winkelwagen.Producten.Remove(winkelwagenItem);
                return winkelwagen;
            }
            finally
            {
                //Herberekenen van de totaal prijs
                winkelwagen.TotaalPrijs = calculator.CalculateCost(winkelwagen);
                SaveContext();
            }
        }

        private Winkelwagen CheckIfWinkelwagenExists(string userId, Winkelwagen winkelwagen)
        {
            if (winkelwagen == null)
            {
                var klant = context.Klanten
                    .Include(a => a.Winkelwagens)
                    .FirstOrDefault(a => a.AzureId == userId);
                winkelwagen = new Winkelwagen()
                {
                    Datum = new DateTime(),
                    Producten = new List<WinkelwagenItem>()
                };
                klant.Winkelwagens.Add(winkelwagen);
            }

            return winkelwagen;
        }
    }
}

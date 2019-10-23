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
    class WinkelwagenFacade
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
        public Winkelwagen GetBagForCustomer(int custId)
        {
            var customer = context.Klanten
                .Include(d => d.Winkelwagens)
                .ThenInclude(b => b.Producten)
                .ThenInclude(i => i.Product)
                .SingleOrDefault(d => d.Id == custId);

            if (customer.Winkelwagens == null || customer.Winkelwagens.Count == 0)
            {
                var bag = new Winkelwagen()
                {
                    Datum = DateTime.Now
                };
                customer.Winkelwagens.Add(bag);
                context.SaveChanges();
            }
            return customer.Winkelwagens
                .OrderByDescending(d => d.Datum)
                .FirstOrDefault();
        }


        /// <summary>
        /// Voeg een product toe in een mandje en bereken de totaalprijs.
        /// </summary>
        /// <param name="Id">ID van het mandje</param>
        /// <param name="prodId">ID van het product</param>
        /// <param name="count">Aantal exemplaren van het betreffende product</param>
        /// <returns></returns>
        public Winkelwagen AddProduct(int Id, int prodId, int count)
        {
            var winkelwagen = context.Winkelwagens
                .FirstOrDefault(d => d.Id == Id);

            try
            {
                foreach (var product in winkelwagen.Producten)
                {
                    if (product.Id == prodId)
                    {
                        product.Aantal += count;
                        return winkelwagen;
                    }
                }
                //not found, create new:
                var product2 = new WinkelwagenItem()
                {

                    Product = context.Trajecten.Find(prodId),
                    Aantal = count
                };
                winkelwagen.Producten.Add(product2);
                return winkelwagen;
            }
            finally
            {
                //Herberekenen van de totaal prijs
                winkelwagen.TotaalPrijs = calculator.CalculateCost(winkelwagen);
                context.SaveChanges();
            }
        }
    }
}

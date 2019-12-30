using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_layer.Repositories
{
    public class WinkelwagenRepository : IWinkelwagenRepository
    {
        private readonly DatabaseContext _context;
        public WinkelwagenRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Winkelwagen GetWinkelwagenByKlantId(string custId)
        {
            var klant = _context.Klanten
                .Include(d => d.Winkelwagens)
                .ThenInclude(b => b.Producten)
                .ThenInclude(i => i.Product)
                .ThenInclude(d => (d as Traject).Cursussen)
                .SingleOrDefault(d => d.AzureId == custId);

            if (klant.Winkelwagens == null || klant.Winkelwagens.Count == 0)
                return null;

            return klant.Winkelwagens
                .OrderByDescending(d => d.Datum)
                .FirstOrDefault();
        }

        public Winkelwagen CreateWinkelwagen(Klant klant)
        {
             var winkelwagen = new Winkelwagen()
             {
                 Datum = DateTime.Now,
                 Producten = new List<WinkelwagenItem>()
             };
             klant.Winkelwagens.Add(winkelwagen);
            return klant.Winkelwagens
                .OrderByDescending(d => d.Datum)
                .FirstOrDefault();
        }

        public Winkelwagen AddProduct(string userId, int prodId, int count, string type)
        {
            var winkelwagen = _context.Winkelwagens
                .Include(a => a.Producten)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(d => d.Klant.AzureId == userId);
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
                    product2.Product = _context.Trajecten.Find(prodId);
                    product2.Aantal = count;
                }
                else if (type == "Cursus")
                {
                    product2.Product = _context.Cursussen.Find(prodId);
                    product2.Aantal = count;
                }
                winkelwagen.Producten.Add(product2);
                return winkelwagen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Winkelwagen DeleteProduct(string userId, int prodId)
        {
            var winkelwagen = _context.Winkelwagens
                .Include(a => a.Producten)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(d => d.Klant.AzureId == userId);
            try
            {
                var winkelwagenItem = winkelwagen.Producten.FirstOrDefault(a => a.Id == prodId);
                winkelwagen.Producten.Remove(winkelwagenItem);
                return winkelwagen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Winkelwagen UpdateProduct(string userId, int prodId, int count)
        {
            var winkelwagen = _context.Winkelwagens
                .Include(a => a.Producten)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(d => d.Klant.AzureId == userId);
            try
            {
                foreach (var product in winkelwagen.Producten)
                {
                    if (product.Product.ID == prodId)
                    {
                        product.Aantal = count;
                        return winkelwagen;
                    }
                }
                return winkelwagen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void SaveChanges()
        {
            try
            {
                var changes = _context.SaveChanges();
                if (changes == 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }
    }
}

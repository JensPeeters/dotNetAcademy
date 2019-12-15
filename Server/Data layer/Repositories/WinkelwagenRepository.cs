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

            klant = CheckIfKlantExists(custId, klant);
            CreateWinkelwagenIfNull(klant);
            return klant.Winkelwagens
                .OrderByDescending(d => d.Datum)
                .FirstOrDefault();
        }

        private void CreateWinkelwagenIfNull(Klant klant)
        {
            if (klant.Winkelwagens == null || klant.Winkelwagens.Count == 0)
            {
                var winkelwagen = new Winkelwagen()
                {
                    Datum = DateTime.Now,
                    Producten = new List<WinkelwagenItem>()
                };
                klant.Winkelwagens.Add(winkelwagen);
            }
        }

        private Klant CheckIfKlantExists(string custId, Klant klant)
        {
            if (klant == null)
            {
                klant = new Klant()
                {
                    AzureId = custId,
                    Winkelwagens = new List<Winkelwagen>()
                };
                _context.Klanten.Add(klant);
            }
            return klant;
        }

        public Winkelwagen AddProduct(string userId, int prodId, int count, string type)
        {
            var winkelwagen = _context.Winkelwagens
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

        private Winkelwagen CheckIfWinkelwagenExists(string userId, Winkelwagen winkelwagen)
        {
            if (winkelwagen == null)
            {
                var klant = _context.Klanten
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

        public Winkelwagen DeleteProduct(string userId, int prodId)
        {
            var winkelwagen = _context.Winkelwagens
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
            winkelwagen = CheckIfWinkelwagenExists(userId, winkelwagen);
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
            if (_context.SaveChanges() > 0)
            {
                _context.SaveChanges();
            }
        }
    }
}

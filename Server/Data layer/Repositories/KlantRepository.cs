using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_layer.Repositories
{
    public class KlantRepository : IKlantRepository
    {
        private readonly DatabaseContext _context;
        public KlantRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Klant GetKlantByID(string klantId)
        {
            var klant = _context.Klanten.FirstOrDefault(d => d.AzureId == klantId);

            if (klant == null)
                return null;

            return klant;
        }
        public Klant CreateKlant(Klant klant)
        {
            var existingKlant = _context.Klanten.FirstOrDefault(d => d.AzureId == klant.AzureId);

            if (existingKlant != null)
                return null;

            _context.Klanten.Add(klant);

            SaveChanges();

            var newKlant = CreateNewWinkelwagenForKlant(klant);

            return newKlant;
        }

        public Klant CreateNewWinkelwagenForKlant(Klant klant)
        {
            var newKlant = _context.Klanten.Include(d => d.Winkelwagens)
                                        .FirstOrDefault(d => d.AzureId == klant.AzureId);

            var winkelwagen = new Winkelwagen()
            {
                Datum = DateTime.Now,
                Producten = new List<WinkelwagenItem>()
            };

            newKlant.Winkelwagens.Add(winkelwagen);

            return newKlant;
        }

        public Klant DeleteKlant(string klantId)
        {
            var deletedKlant = _context.Klanten.FirstOrDefault(a => a.AzureId == klantId);
            if (deletedKlant == null)
                return null;
            var deletedWinkelmand = _context.Winkelwagens.FirstOrDefault(a => a.Klant == deletedKlant);
            try
            {
                _context.Winkelwagens.Remove(deletedWinkelmand);
                _context.Klanten.Remove(deletedKlant);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return deletedKlant;
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

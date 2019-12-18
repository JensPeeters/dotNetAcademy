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
            var klant = _context.Klanten.Include(a => a.Winkelwagens)
                                        .FirstOrDefault(d => d.AzureId == klantId);

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

            var newKlant = CreateNewWinkelwagenForKlant(klant);

            return newKlant;
        }

        public Klant CreateNewWinkelwagenForKlant(Klant klant)
        {
            var winkelwagen = new Winkelwagen()
            {
                Datum = DateTime.Now,
                Producten = new List<WinkelwagenItem>()
            };
            klant.Winkelwagens = new List<Winkelwagen>();
            klant.Winkelwagens.Add(winkelwagen);

            return klant;
        }

        public Klant DeleteKlant(string klantId)
        {
            var deletedKlant = _context.Klanten.FirstOrDefault(a => a.AzureId == klantId);
            if (deletedKlant == null)
                return null;
            var deletedWinkelmand = _context.Winkelwagens.Include(a => a.Producten)
                                                         .FirstOrDefault(a => a.Klant == deletedKlant);
            try
            {
                deletedWinkelmand.Producten = new List<WinkelwagenItem>();
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

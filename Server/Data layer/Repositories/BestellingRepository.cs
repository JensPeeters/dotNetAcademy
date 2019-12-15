using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Data_layer.Repositories
{
    public class BestellingRepository : IBestellingRepository
    {
        private readonly DatabaseContext _context;
        public BestellingRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Bestelling AddBestellingToCustomer(Bestelling bestelling)
        {
            _context.Bestellingen.Add(bestelling);
            return bestelling;
        }

        public List<Bestelling> GetBestellingenByCustomerId(string custId)
        {
            return _context.Bestellingen
                    .Where(d => d.Klant.AzureId == custId)
                    .Include(b => b.Producten)
                    .ThenInclude(i => i.Product)
                    .OrderByDescending(d => d.Datum)
                    .ToList();
        }

        public Bestelling GetBestellingById(int bestellingId)
        {
            return _context.Bestellingen
                    .Where(d => d.Id == bestellingId)
                    .Include(b => b.Producten)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault();
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

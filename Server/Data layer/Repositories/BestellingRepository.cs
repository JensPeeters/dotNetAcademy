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
            this._context = context;
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
    }
}

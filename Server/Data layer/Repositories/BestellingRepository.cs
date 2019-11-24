using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_layer.Repositories
{
    public class BestellingRepository : IBestellingRepository
    {
        private readonly DatabaseContext _context;
        public BestellingRepository(DatabaseContext context)
        {
            this._context = context;
        }
        public List<Bestelling> GetBestellingen(string custId)
        {
            var klant = _context.Klanten
                .Include(d => d.Bestellingen)
                .ThenInclude(b => b.Producten)
                .ThenInclude(i => i.Product)
                .SingleOrDefault(d => d.AzureId == custId);

            if (klant == null)
                return null;

            return klant.Bestellingen.OrderByDescending(d => d.Datum).ToList();
        }
    }
}

using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_layer.Repositories
{
    public class BestellingRepository
    {
        private readonly DatabaseContext context;
        public BestellingRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public List<Bestelling> GetBestellingen(string custId)
        {
            var klant = context.Klanten
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

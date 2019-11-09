using Data_layer;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_layer
{
    public class BestellingFacade
    {
        private readonly DatabaseContext context;
        public BestellingFacade(DatabaseContext context)
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

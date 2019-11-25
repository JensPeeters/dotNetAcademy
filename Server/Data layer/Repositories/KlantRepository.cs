using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_layer.Repositories
{
    public class KlantRepository
    {
        private readonly DatabaseContext context;
        public KlantRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public Klant GetKlantByID(string klantId)
        {
            var klant = context.Klanten.FirstOrDefault(d => d.AzureId == klantId);

            if (klant == null)
                return null;

            return klant;
        }
        public Klant CreateKlant(Klant klant)
        {
            var existingKlant = context.Klanten.FirstOrDefault(d => d.AzureId == klant.AzureId);

            if (existingKlant == null)
                return null;

            context.Klanten.Add(klant);

            try
            {
                SaveChanges();
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return klant;
        }

        private void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}

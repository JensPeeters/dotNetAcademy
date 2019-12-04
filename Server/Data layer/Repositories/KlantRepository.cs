using Data_layer.Interfaces;
using Data_layer.Model;
using System;
using System.Linq;

namespace Data_layer.Repositories
{
    public class KlantRepository : IKlantRepository
    {
        private readonly DatabaseContext _context;
        public KlantRepository(DatabaseContext context)
        {
            this._context = context;
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

            return klant;
        }

        public Klant DeleteKlant(string klantId)
        {
            var deletedKlant = _context.Klanten.FirstOrDefault(a => a.AzureId == klantId);
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

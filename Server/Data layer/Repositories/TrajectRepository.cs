using Data_layer.Filter.ProductenFilters;
using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_layer.Repositories
{
    public class TrajectRepository : ITrajectRepository
    {
        private readonly DatabaseContext _context;
        private readonly IContextFilter _sortFilter;

        public TrajectRepository(DatabaseContext context, IContextFilter sortFilter)
        {
            this._context = context;
            this._sortFilter = sortFilter;
        }
        public List<Traject> GetTrajecten(TrajectFilter filter)
        {
            IQueryable<Product> query = _context.Trajecten.Include(a => a.Cursussen);
            query = _sortFilter.Filter(filter, query);
            return query.Select(traject => new Traject {
                Beschrijving = traject.Beschrijving,
                Categorie = traject.Categorie,
                FotoURLCard = traject.FotoURLCard,
                IsBuyable = traject.IsBuyable,
                Cursussen = (traject as Traject).Cursussen,
                ID = traject.ID,
                LangeBeschrijving = traject.LangeBeschrijving,
                Prijs = traject.Prijs,
                Titel = traject.Titel,
                Type = traject.Type
            }).ToList();
        }

        public Traject GetTrajectByTitel(string titel)
        {
            return _context.Trajecten.FirstOrDefault(a => a.Titel == titel);
        }

        public Traject GetTrajectById(int id)
        {
            return _context.Trajecten.Include(a => a.Cursussen)
                                        .FirstOrDefault(a => a.ID == id);
        }

        public Traject AddTraject(Traject traject)
        {
            _context.Trajecten.Add(traject);
            return traject;
        }

        public void SaveChanges()
        {
            if (_context.SaveChanges() > 0)
            {
                _context.SaveChanges();
            }
        }


        public Traject DeleteTraject(int id)
        {
            var deletedTraject = _context.Trajecten.FirstOrDefault(a => a.ID == id);
            try
            {
                deletedTraject.IsBuyable = !deletedTraject.IsBuyable;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return deletedTraject;
        }

        public Traject UpdateTraject(Traject traject)
        {
            var existingTraject = _context.Trajecten.FirstOrDefault(a => a.ID == traject.ID);
            if (existingTraject == null)
                return null;
            existingTraject.Beschrijving = traject.Beschrijving;
            existingTraject.Categorie = traject.Categorie;
            existingTraject.FotoURLCard = traject.FotoURLCard;
            existingTraject.Cursussen = traject.Cursussen;
            existingTraject.LangeBeschrijving = traject.LangeBeschrijving;
            existingTraject.Prijs = traject.Prijs;
            existingTraject.Titel = traject.Titel;
            existingTraject.Type = traject.Type;
            return traject;
        }
    }
}

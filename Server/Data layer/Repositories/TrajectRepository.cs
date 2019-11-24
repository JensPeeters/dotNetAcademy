using Data_layer.Filter;
using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public List<Traject> GetTrajecten(ProductFilterQuery filter)
        {
            IQueryable<Product> query = _context.Trajecten.Include(a => a.Cursussen);
            query = _sortFilter.Filter(filter, query);
            return query.Select(traject => new Traject {
                Beschrijving = traject.Beschrijving,
                Categorie = traject.Categorie,
                FotoURLCard = traject.FotoURLCard,
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
            var existingTraject = _context.Trajecten.FirstOrDefault(o => o.Titel == traject.Titel);
            if (existingTraject != null)
                return null;
            _context.Trajecten.Add(traject);
            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return traject;
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }


        public Traject DeleteTraject(int id)
        {
            var deletedTraject = _context.Trajecten.FirstOrDefault(a => a.ID == id);
            if (deletedTraject == null)
                return null;

            _context.Trajecten.Remove(deletedTraject);
            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return deletedTraject;
        }

        public Traject UpdateTraject(Traject traject)
        {
            var existingTraject = _context.Trajecten.FirstOrDefault(a => a.ID == traject.ID);
            if (existingTraject == null)
                return null;
            _context.Entry(existingTraject).State = EntityState.Detached;
            _context.Trajecten.Update(traject);
            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return traject;
        }
    }
}

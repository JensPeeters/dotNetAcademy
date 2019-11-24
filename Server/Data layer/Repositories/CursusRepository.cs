using Data_layer.Filter;
using Data_layer.Filter.ProductenFilters;
using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_layer.Repositories
{
    public class CursusRepository : ICursusRepository
    {
        private readonly DatabaseContext _context;
        private readonly IContextFilter _sortFilter;

        public CursusRepository(DatabaseContext context, IContextFilter sortFilter)
        {
            this._context = context;
            this._sortFilter = sortFilter;
        }
        public List<Cursus> GetCursussen(CursusFilter filter)
        {
            IQueryable<Product> query = _context.Cursussen;
            query = _sortFilter.Filter(filter, query);
            return query.Select(cursus => new Cursus
            {
                Beschrijving = cursus.Beschrijving,
                Categorie = cursus.Categorie,
                FotoURLCard = cursus.FotoURLCard,
                ID = cursus.ID,
                LangeBeschrijving = cursus.LangeBeschrijving,
                Prijs = cursus.Prijs,
                Titel = cursus.Titel,
                Type = cursus.Type
            }).ToList();
        }

        public Cursus GetCursusByTitel(string titel)
        {
            return _context.Cursussen.FirstOrDefault(a => a.Titel == titel);
        }

        public Cursus GetCursusById(int id)
        {
            return _context.Cursussen.FirstOrDefault(a => a.ID == id);
        }

        public Cursus AddCursus(Cursus cursus)
        {
            var existingCursus = _context.Cursussen.FirstOrDefault(o => o.Titel == cursus.Titel);
            if (existingCursus != null)
                return null;
            _context.Cursussen.Add(cursus);
            return cursus;
        }

        public void SaveChanges()
        {
            if (_context.SaveChanges() > 0)
            {
                _context.SaveChanges();
            }
        }


        public Cursus DeleteCursus(int id)
        {
            var deletedCursus = _context.Cursussen.FirstOrDefault(a => a.ID == id);
            if (deletedCursus == null)
                return null;
            _context.Cursussen.Remove(deletedCursus);
            return deletedCursus;
        }

        public Cursus UpdateCursus(Cursus cursus)
        {
            var existingCursus = _context.Cursussen.FirstOrDefault(a => a.ID == cursus.ID);
            if (existingCursus == null)
                return null;
            _context.Entry(existingCursus).State = EntityState.Detached;
            _context.Cursussen.Update(cursus);
            return cursus;
        }
    }
}

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
    public class CursusRepository : ICursusRepository
    {
        private readonly DatabaseContext context;
        private readonly ISortFilter sortFilter;

        public CursusRepository(DatabaseContext context, ISortFilter sortFilter)
        {
            this.context = context;
            this.sortFilter = sortFilter;
        }
        public List<Cursus> GetCursussen(ProductFilter filter)
        {
            IQueryable<Product> query = context.Cursussen;
            query = sortFilter.Filter(filter, query);
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
            return context.Cursussen.FirstOrDefault(a => a.Titel == titel);
        }

        public Cursus GetCursusById(int id)
        {
            return context.Cursussen.FirstOrDefault(a => a.ID == id);
        }

        public Cursus AddCursus(Cursus cursus)
        {
            var existingCursus = context.Cursussen.FirstOrDefault(o => o.Titel == cursus.Titel);
            if (existingCursus != null)
                return null;
            context.Cursussen.Add(cursus);
            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cursus;
        }

        private void SaveChanges()
        {
            context.SaveChanges();
        }


        public Cursus DeleteCursus(int id)
        {
            var deletedCursus = context.Cursussen.FirstOrDefault(a => a.ID == id);
            if (deletedCursus == null)
                return null;

            context.Cursussen.Remove(deletedCursus);
            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return deletedCursus;
        }

        public Cursus UpdateCursus(Cursus cursus)
        {
            var existingCursus = context.Cursussen.FirstOrDefault(a => a.ID == cursus.ID);
            if (existingCursus == null)
                return null;
            context.Entry(existingCursus).State = EntityState.Detached;
            context.Cursussen.Update(cursus);
            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cursus;
        }
    }
}

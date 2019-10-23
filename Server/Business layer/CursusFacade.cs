using Data_layer;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_layer
{
    public class CursusFacade
    {
        private readonly DatabaseContext context;

        public CursusFacade(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Cursus> GetCursussen(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            IQueryable<Cursus> query = context.Cursussen;
            if (!string.IsNullOrEmpty(type))
                query = query.Where(b => b.Type == type);

            if (!string.IsNullOrEmpty(titel))
                query = query.Where(b => b.Titel == titel);

            if (string.IsNullOrEmpty(sortBy))
                sortBy = "id";

            switch (sortBy.ToLower())
            {
                case "id":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.ID);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.ID);
                    break;
                case "prijs":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Prijs);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.Prijs);
                    break;
                case "titel":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Titel);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.Titel);
                    break;
                case "type":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Type);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.Type);
                    break;
                default:
                    if (direction == "asc")
                        query = query.OrderBy(b => b.ID);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.ID);
                    break;
            }
            if (pageSize > 16)
                pageSize = 16;

            query = query.Skip(page * pageSize);
            query = query.Take(pageSize);

            return query.ToList();
        }

        public Cursus GetCursus(int id)
        {
            return context.Cursussen.FirstOrDefault(a => a.ID == id);
        }

        public Cursus AddCursus(Cursus cursus)
        {
            var createdCursus = context.Cursussen.FirstOrDefault(o => o.Titel == cursus.Titel);
            if (createdCursus != null)
                return null;
            context.Cursussen.Add(createdCursus);
            context.SaveChanges();
            return createdCursus;
        }

        public Cursus DeleteCursus(int id)
        {
            var deletedCursus = context.Cursussen.FirstOrDefault(a => a.ID == id);
            if (deletedCursus == null)
                return null;

            context.Cursussen.Remove(deletedCursus);
            context.SaveChanges();
            return deletedCursus;
        }

        public Cursus UpdateCursus(Cursus cursus)
        {
            context.Cursussen.Update(cursus);
            context.SaveChanges();
            return cursus;
        }
    }
}

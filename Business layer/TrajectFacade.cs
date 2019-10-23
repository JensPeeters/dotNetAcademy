using Data_layer;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_layer
{
    public class TrajectFacade
    {
        private readonly DatabaseContext context;

        public TrajectFacade(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Traject> GetTrajecten(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            IQueryable<Traject> query = context.Trajecten.Include(a => a.Cursussen);
            if (!string.IsNullOrEmpty(type))
                query = query.Where(b => b.Type.ToLower().Contains(type.ToLower().Trim()));

            if (!string.IsNullOrEmpty(titel))
                query = query.Where(b => b.Titel.ToLower().Contains(titel.ToLower().Trim()));

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

        public Traject GetTraject(int id)
        {
            return context.Trajecten.Include(a => a.Cursussen).FirstOrDefault(a => a.ID == id);
        }

        public Traject AddTraject(Traject traject)
        {
            var createdTraject = context.Trajecten.FirstOrDefault(o => o.Titel == traject.Titel);
            if (createdTraject != null)
                return null;
            context.Trajecten.Add(createdTraject);
            context.SaveChanges();
            return createdTraject;
        }

        public Traject DeleteTraject(int id)
        {
            var deletedTraject = context.Trajecten.Include(a => a.Cursussen)
                .FirstOrDefault(a => a.ID == id);
            if (deletedTraject == null)
                return null;
            
            context.Trajecten.Remove(deletedTraject);
            context.SaveChanges();
            return deletedTraject;
        }

        public Traject UpdateTraject(Traject traject)
        {
            context.Trajecten.Update(traject);
            context.SaveChanges();
            return traject;
        }
    }
}

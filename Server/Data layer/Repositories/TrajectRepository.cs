using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_layer.Repositories
{
    public class TrajectRepository
    {
        private readonly DatabaseContext context;
        public TrajectRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public IQueryable<Traject> GetTrajecten()
        {
            return context.Trajecten.Include(a => a.Cursussen);
        }

        public Traject GetTrajectByTitel(string titel)
        {
            return context.Trajecten.FirstOrDefault(a => a.Titel == titel);
        }

        public Traject GetTrajectById(int id)
        {
            return context.Trajecten.Include(a => a.Cursussen)
                                        .FirstOrDefault(a => a.ID == id);
        }

        public Traject AddTraject(Traject traject)
        {
            var existingTraject = context.Trajecten.FirstOrDefault(o => o.Titel == traject.Titel);
            if (existingTraject != null)
                return null;
            context.Trajecten.Add(traject);
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
            context.SaveChanges();
        }


        public Traject DeleteTraject(int id)
        {
            var deletedTraject = context.Trajecten.FirstOrDefault(a => a.ID == id);
            if (deletedTraject == null)
                return null;

            context.Trajecten.Remove(deletedTraject);
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
            var existingTraject = context.Trajecten.FirstOrDefault(a => a.ID == traject.ID);
            if (existingTraject == null)
                return null;
            context.Entry(existingTraject).State = EntityState.Detached;
            context.Trajecten.Update(traject);
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

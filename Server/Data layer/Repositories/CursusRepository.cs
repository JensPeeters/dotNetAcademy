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
        public CursusRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public Product AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByTitel(string titel)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            return context.Cursussen.ToList();
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
        //public IQueryable<Cursus> GetCursussen()
        //{
        //    
        //}

        //public Cursus GetCursusByTitel(string titel)
        //{
        //    return context.Cursussen.FirstOrDefault(a => a.Titel == titel);
        //}

        //public Cursus GetCursusById(int id)
        //{
        //    return context.Cursussen.FirstOrDefault(a => a.ID == id);
        //}

        //public Cursus AddCursus(Cursus cursus)
        //{
        //    var existingCursus = context.Cursussen.FirstOrDefault(o => o.Titel == cursus.Titel);
        //    if (existingCursus != null)
        //        return null;
        //    context.Cursussen.Add(cursus);
        //    try
        //    {
        //        SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return cursus;
        //}

        //private void SaveChanges()
        //{
        //    context.SaveChanges();
        //}


        //public Cursus DeleteCursus(int id)
        //{
        //    var deletedCursus = context.Cursussen.FirstOrDefault(a => a.ID == id);
        //    if (deletedCursus == null)
        //        return null;

        //    context.Cursussen.Remove(deletedCursus);
        //    try
        //    {
        //        SaveChanges();
        //    }
        //    catch(Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return deletedCursus;
        //}

        //public Cursus UpdateCursus(Cursus cursus)
        //{
        //    var existingCursus = context.Cursussen.FirstOrDefault(a => a.ID == cursus.ID);
        //    if (existingCursus == null)
        //        return null;
        //    context.Entry(existingCursus).State = EntityState.Detached;
        //    context.Cursussen.Update(cursus);
        //    try
        //    {
        //        SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return cursus;
        //}
    }
}

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
            _context = context;
            _sortFilter = sortFilter;
        }

        public List<string> GetTrajectTypes()
        {
            List<string> typesList = new List<string>();
            typesList.Add("Aanbevolen");
            IQueryable<Product> query = _context.Trajecten;
            foreach (Traject traject in query)
            {
                if (!typesList.Contains(traject.Type))
                {
                    typesList.Add(traject.Type);
                }
            }
            return typesList;
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
                Type = traject.Type,
                OrderNumber = traject.OrderNumber
            }).ToList();
        }

        public List<Traject> GetBuyableTrajecten(TrajectFilter filter)
        {
            IQueryable<Product> query = _context.Trajecten
                                                .Where(a => a.IsBuyable == true)
                                                .Include(a => a.Cursussen);
            query = _sortFilter.Filter(filter, query);
            return query.Select(traject => new Traject
            {
                Beschrijving = traject.Beschrijving,
                Categorie = traject.Categorie,
                FotoURLCard = traject.FotoURLCard,
                IsBuyable = traject.IsBuyable,
                Cursussen = (traject as Traject).Cursussen,
                ID = traject.ID,
                LangeBeschrijving = traject.LangeBeschrijving,
                Prijs = traject.Prijs,
                Titel = traject.Titel,
                Type = traject.Type,
                OrderNumber = traject.OrderNumber
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
            var tempList = traject.Cursussen;
            traject.Cursussen = new List<Cursus>();
            foreach (var cursus in tempList)
            {
                traject.Cursussen.Add(_context.Cursussen.Where(a => a.ID == cursus.ID).FirstOrDefault());
            }
            _context.Trajecten.Add(traject);
            traject.OrderNumber = _context.Trajecten.Count() + 1;
            return traject;
        }

        public void SaveChanges()
        {
            try
            {
                var changes = _context.SaveChanges();
                if (changes == 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
            var existingOrderNumber = existingTraject.OrderNumber;
            if (existingTraject == null)
                return null;
            var tempList = traject.Cursussen;
            existingTraject.Cursussen = new List<Cursus>();
            foreach (var cursus in tempList)
            {
                existingTraject.Cursussen.Add(_context.Cursussen.Where(a => a.ID == cursus.ID).FirstOrDefault());
            }
            existingTraject.Beschrijving = traject.Beschrijving;
            existingTraject.Categorie = traject.Categorie;
            existingTraject.FotoURLCard = traject.FotoURLCard;
            existingTraject.LangeBeschrijving = traject.LangeBeschrijving;
            existingTraject.Prijs = traject.Prijs;
            existingTraject.Titel = traject.Titel;
            existingTraject.Type = traject.Type;
            if (existingTraject.OrderNumber != traject.OrderNumber)
            {
                IQueryable<Product> query = _context.Trajecten;
                foreach (Traject _traject in query)
                {
                    if (_traject == existingTraject)
                    {
                        existingTraject.OrderNumber = traject.OrderNumber;
                    }
                    else if (traject.OrderNumber > existingOrderNumber)
                    {
                        if (_traject.OrderNumber <= traject.OrderNumber && _traject.OrderNumber > existingOrderNumber)
                        {
                            _traject.OrderNumber--;
                        }
                    }
                    else if (traject.OrderNumber < existingOrderNumber)
                    {
                        if (_traject.OrderNumber >= traject.OrderNumber && _traject.OrderNumber < existingOrderNumber)
                        {
                            _traject.OrderNumber++;
                        }
                    }
                }
            }
            return traject;
        }
        public int GetAmountSold(int id)
        {
            int amount = 0;
            var bestellingen = _context.Bestellingen.Include(a => a.Producten)
                                                    .ThenInclude(i => i.Product)
                                                    .ToList();
            foreach (var bestelling in bestellingen)
            {
                foreach (var product in bestelling.Producten)
                {
                    if (product.Product.ID == id)
                    {
                        amount++;
                    }
                }
            }
            return amount;
        }
    }
}

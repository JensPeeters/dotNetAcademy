using Data_layer.Filter.ProductenFilters;
using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_layer.Repositories
{
    public class CursusRepository : ICursusRepository
    {
        private readonly DatabaseContext _context;
        private readonly IContextFilter _sortFilter;

        public CursusRepository(DatabaseContext context, IContextFilter sortFilter)
        {
            _context = context;
            _sortFilter = sortFilter;
        }

        public List<string> GetCursusTypes()
        {
            List<string> typesList = new List<string>();
            typesList.Add("Aanbevolen");
            IQueryable<Product> query = _context.Cursussen;
            foreach (Cursus cursus in query)
            {
                if (!typesList.Contains(cursus.Type))
                {
                    typesList.Add(cursus.Type);
                }
            }
            return typesList;
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
                IsBuyable = cursus.IsBuyable,
                LangeBeschrijving = cursus.LangeBeschrijving,
                Prijs = cursus.Prijs,
                Titel = cursus.Titel,
                Type = cursus.Type,
                OrderNumber = cursus.OrderNumber
            }).ToList();
        }

        public List<Cursus> GetBuyableCursussen(CursusFilter filter)
        {
            IQueryable<Product> query = _context.Cursussen.Where(a => a.IsBuyable == true);
            query = _sortFilter.Filter(filter, query);
            return query.Select(cursus => new Cursus
            {
                Beschrijving = cursus.Beschrijving,
                Categorie = cursus.Categorie,
                FotoURLCard = cursus.FotoURLCard,
                ID = cursus.ID,
                IsBuyable = cursus.IsBuyable,
                LangeBeschrijving = cursus.LangeBeschrijving,
                Prijs = cursus.Prijs,
                Titel = cursus.Titel,
                Type = cursus.Type,
                OrderNumber = cursus.OrderNumber
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
            _context.Cursussen.Add(cursus);
            cursus.OrderNumber = _context.Cursussen.Count() + 1;
            return cursus;
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


        public Cursus DeleteCursus(int id)
        {
            var deletedCursus = _context.Cursussen.FirstOrDefault(a => a.ID == id);
            try
            {
                deletedCursus.IsBuyable = !deletedCursus.IsBuyable;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return deletedCursus;
        }

        public Cursus UpdateCursus(Cursus cursus)
        {
            var existingCursus = _context.Cursussen.FirstOrDefault(a => a.ID == cursus.ID);
            var existingOrderNumber = existingCursus.OrderNumber;
            if (existingCursus == null)
                return null;
            existingCursus.Beschrijving = cursus.Beschrijving;
            existingCursus.Categorie = cursus.Categorie;
            existingCursus.FotoURLCard = cursus.FotoURLCard;
            existingCursus.LangeBeschrijving = cursus.LangeBeschrijving;
            existingCursus.Prijs = cursus.Prijs;
            existingCursus.Titel = cursus.Titel;
            existingCursus.Type = cursus.Type;
            if (existingCursus.OrderNumber != cursus.OrderNumber)
            {
                IQueryable<Product> query = _context.Cursussen;
                foreach(Cursus _cursus in query)
                {
                    if (_cursus == existingCursus)
                    {
                        existingCursus.OrderNumber = cursus.OrderNumber;
                    }
                    else if (cursus.OrderNumber > existingOrderNumber)
                    {
                        if (_cursus.OrderNumber <= cursus.OrderNumber && _cursus.OrderNumber > existingOrderNumber)
                        {
                            _cursus.OrderNumber--;
                        }
                    }
                    else if (cursus.OrderNumber < existingOrderNumber)
                    {
                        if (_cursus.OrderNumber >= cursus.OrderNumber && _cursus.OrderNumber < existingOrderNumber)
                        {
                            _cursus.OrderNumber++;
                        }
                    }
                }
            }
            return cursus;
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

using Business_layer.DTO;
using Business_layer.Interfaces;
using Data_layer.Filter.ProductenFilters;
using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business_layer
{
    public class CursusFacade : ICursusFacade
    {
        private readonly ICursusRepository _repositoryCursus;

        public CursusFacade(ICursusRepository repositoryCursus)
        {
            this._repositoryCursus = repositoryCursus;
        }

        public List<CursusDTO> GetCursussen(CursusFilter filter)
        {
            return _repositoryCursus.GetCursussen(filter)
                        .Select(cursus => ConvertCursusToDTO(cursus))
                        .ToList();
        }

        public CursusDTO GetCursus(int id)
        {
            var cursus = _repositoryCursus.GetCursusById(id);
            if (cursus == null)
                return null;
            return ConvertCursusToDTO(cursus);
        }

        private static CursusDTO ConvertCursusToDTO(Cursus cursus)
        {
            return new CursusDTO()
            {
                Beschrijving = cursus.Beschrijving,
                Categorie = cursus.Categorie,
                FotoURLCard = cursus.FotoURLCard,
                ID = cursus.ID,
                IsBuyable = cursus.IsBuyable,
                LangeBeschrijving = cursus.LangeBeschrijving,
                Prijs = cursus.Prijs,
                Titel = cursus.Titel,
                Type = cursus.Type
            };
        }

        public CursusDTO AddCursus(CursusCreateUpdateDTO cursus)
        {
            var newCursus = ConvertCreateUpdateDTOToCursus(cursus);
            var createdCursus = _repositoryCursus.AddCursus(newCursus);
            try
            {
                _repositoryCursus.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertCursusToDTO(createdCursus);
        }

        private static Cursus ConvertCreateUpdateDTOToCursus(CursusCreateUpdateDTO cursus)
        {
            return new Cursus()
            {
                Beschrijving = cursus.Beschrijving,
                Categorie = cursus.Categorie,
                FotoURLCard = cursus.FotoURLCard,
                LangeBeschrijving = cursus.LangeBeschrijving,
                IsBuyable = cursus.IsBuyable,
                Prijs = cursus.Prijs,
                Titel = cursus.Titel,
                Type = cursus.Type
            };
        }

        public CursusDTO DeleteCursus(int id)
        {
            var deletedCursus = _repositoryCursus.DeleteCursus(id);
            if (deletedCursus == null)
                return null;
            try
            {
                _repositoryCursus.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertCursusToDTO(deletedCursus);
        }

        public CursusDTO UpdateCursus(CursusCreateUpdateDTO cursus, int id)
        {
            var newCursus = ConvertCreateUpdateDTOToCursus(cursus);
            newCursus.ID = id;
            var updatedCursus = _repositoryCursus.UpdateCursus(newCursus);
            if (updatedCursus == null)
                return null;
            try
            {
                _repositoryCursus.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertCursusToDTO(updatedCursus);
        }
    }
}

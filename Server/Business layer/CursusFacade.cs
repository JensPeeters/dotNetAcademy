using Business_layer.DTO;
using Data_layer;
using Data_layer.Filter;
using Data_layer.Interfaces;
using Data_layer.Model;
using Data_layer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_layer
{
    public class CursusFacade
    {
        private readonly CursusRepository repository;

        public CursusFacade(CursusRepository repository)
        {
            this.repository = repository;
        }

        public List<CursusDTO> GetCursussen(ProductFilter filter)
        {
            var cursussen = new List<CursusDTO>();
            foreach (Cursus cursus in repository.GetCursussen(filter))
            {
                cursussen.Add(ConvertCursusToDTO(cursus));
            }
            return cursussen;
        }

        public CursusDTO GetCursus(int id)
        {
            var cursus = repository.GetCursusById(id);
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
                LangeBeschrijving = cursus.LangeBeschrijving,
                Prijs = cursus.Prijs,
                Titel = cursus.Titel,
                Type = cursus.Type
            };
        }

        public CursusDTO AddCursus(CursusCreateUpdateDTO cursus)
        {
            var existingCursus = repository.GetCursusByTitel(cursus.Titel);
            if (existingCursus != null)
                return null;
            var newCursus = ConvertCreateUpdateDTOToCursus(cursus);
            var createdCursus = repository.AddCursus(newCursus);
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
                Prijs = cursus.Prijs,
                Titel = cursus.Titel,
                Type = cursus.Type
            };
        }

        public CursusDTO DeleteCursus(int id)
        {
            var deletedCursus = repository.DeleteCursus(id);
            if (deletedCursus == null)
                return null;
            return ConvertCursusToDTO(deletedCursus);
        }

        public CursusDTO UpdateCursus(CursusCreateUpdateDTO cursus, int id)
        {
            var newCursus = ConvertCreateUpdateDTOToCursus(cursus);
            newCursus.ID = id;
            var updatedCursus = repository.UpdateCursus(newCursus);
            if (updatedCursus == null)
                return null;
            return ConvertCursusToDTO(updatedCursus);
        }
    }
}

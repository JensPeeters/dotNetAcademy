using Business_layer.DTO;
using Data_layer;
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

        public List<CursusDTO> GetCursussen(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            IQueryable<Cursus> query = repository.GetCursussen();
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
            query = query.Skip(page * pageSize);
            query = query.Take(pageSize);

            var cursussen = new List<CursusDTO>();
            foreach (var cursus in query.ToList())
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

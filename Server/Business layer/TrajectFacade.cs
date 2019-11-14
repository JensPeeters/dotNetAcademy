using Business_layer.DTO;
using Data_layer;
using Data_layer.Model;
using Data_layer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_layer
{
    public class TrajectFacade
    {
        private readonly TrajectRepository repository;

        public TrajectFacade(TrajectRepository repository)
        {
            this.repository = repository;
        }

        public List<TrajectDTO> GetTrajecten(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            IQueryable<Traject> query = repository.GetTrajecten();
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

            var trajecten = new List<TrajectDTO>();
            foreach (var traject in query.ToList())
            {
                trajecten.Add(ConvertTrajectToDTO(traject));
            }
            return trajecten;
        }

        private static TrajectDTO ConvertTrajectToDTO(Traject traject)
        {
            return new TrajectDTO()
            {
                Beschrijving = traject.Beschrijving,
                Categorie = traject.Categorie,
                FotoURLCard = traject.FotoURLCard,
                ID = traject.ID,
                LangeBeschrijving = traject.LangeBeschrijving,
                Prijs = traject.Prijs,
                Titel = traject.Titel,
                Type = traject.Type,
                Cursussen = traject.Cursussen
            };
        }

        public TrajectDTO GetTraject(int id)
        {
            var traject = repository.GetTrajectById(id);
            if (traject == null)
                return null;
            return ConvertTrajectToDTO(traject);
        }

        public TrajectDTO AddTraject(TrajectCreateUpdateDTO traject)
        {
            var existingTraject = repository.GetTrajectByTitel(traject.Titel);
            if (existingTraject != null)
                return null;
            var newTraject = ConvertCreateUpdateDTOToTraject(traject);
            var createdTraject = repository.AddTraject(newTraject);
            return ConvertTrajectToDTO(createdTraject);
        }

        private static Traject ConvertCreateUpdateDTOToTraject(TrajectCreateUpdateDTO traject)
        {
            return new Traject()
            {
                Beschrijving = traject.Beschrijving,
                Categorie = traject.Categorie,
                FotoURLCard = traject.FotoURLCard,
                LangeBeschrijving = traject.LangeBeschrijving,
                Prijs = traject.Prijs,
                Titel = traject.Titel,
                Cursussen = traject.Cursussen,
                Type = traject.Type
            };
        }

        public TrajectDTO DeleteTraject(int id)
        {
            var deletedTraject = repository.DeleteTraject(id);
            if (deletedTraject == null)
                return null;
            return ConvertTrajectToDTO(deletedTraject);
        }

        public TrajectDTO UpdateTraject(TrajectCreateUpdateDTO traject, int id)
        {
            var newTraject = ConvertCreateUpdateDTOToTraject(traject);
            newTraject.ID = id;
            var updatedTraject = repository.UpdateTraject(newTraject);
            if (updatedTraject == null)
                return null;
            return ConvertTrajectToDTO(updatedTraject);
        }
    }
}

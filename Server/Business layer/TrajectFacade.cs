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
    public class TrajectFacade : ITrajectFacade
    {
        private readonly ITrajectRepository _repository;

        public TrajectFacade(ITrajectRepository repository)
        {
            this._repository = repository;
        }

        public List<TrajectDTO> GetTrajecten(TrajectFilter filter)
        {
            return _repository.GetTrajecten(filter)
                        .Select(traject => ConvertTrajectToDTO(traject))
                        .ToList();
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
            var traject = _repository.GetTrajectById(id);
            if (traject == null)
                return null;
            return ConvertTrajectToDTO(traject);
        }

        public TrajectDTO AddTraject(TrajectCreateUpdateDTO traject)
        {
            var newTraject = ConvertCreateUpdateDTOToTraject(traject);
            var createdTraject = _repository.AddTraject(newTraject);
            try
            {
                _repository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
            var deletedTraject = _repository.DeleteTraject(id);
            if (deletedTraject == null)
                return null;
            try
            {
                _repository.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertTrajectToDTO(deletedTraject);
        }

        public TrajectDTO UpdateTraject(TrajectCreateUpdateDTO traject, int id)
        {
            var newTraject = ConvertCreateUpdateDTOToTraject(traject);
            newTraject.ID = id;
            var updatedTraject = _repository.UpdateTraject(newTraject);
            if (updatedTraject == null)
                return null;
            try
            {
                _repository.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertTrajectToDTO(updatedTraject);
        }
    }
}

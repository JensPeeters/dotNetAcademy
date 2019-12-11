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
        private readonly ITrajectRepository _repositoryTraject;

        public TrajectFacade(ITrajectRepository repositoryTraject)
        {
            this._repositoryTraject = repositoryTraject;
        }

        public List<TrajectDTO> GetTrajecten(TrajectFilter filter)
        {
            return _repositoryTraject.GetTrajecten(filter)
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
                IsBuyable = traject.IsBuyable,
                Prijs = traject.Prijs,
                Titel = traject.Titel,
                Type = traject.Type,
                Cursussen = traject.Cursussen
            };
        }

        public TrajectDTO GetTraject(int id)
        {
            var traject = _repositoryTraject.GetTrajectById(id);
            if (traject == null)
                return null;
            return ConvertTrajectToDTO(traject);
        }

        public TrajectDTO AddTraject(TrajectCreateUpdateDTO traject)
        {
            var newTraject = ConvertCreateUpdateDTOToTraject(traject);
            var createdTraject = _repositoryTraject.AddTraject(newTraject);
            try
            {
                _repositoryTraject.SaveChanges();
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
                IsBuyable = traject.IsBuyable,
                Prijs = traject.Prijs,
                Titel = traject.Titel,
                Cursussen = traject.Cursussen,
                Type = traject.Type
            };
        }

        public TrajectDTO DeleteTraject(int id)
        {
            var deletedTraject = _repositoryTraject.DeleteTraject(id);
            if (deletedTraject == null)
                return null;
            try
            {
                _repositoryTraject.SaveChanges();
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
            var updatedTraject = _repositoryTraject.UpdateTraject(newTraject);
            if (updatedTraject == null)
                return null;
            try
            {
                _repositoryTraject.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertTrajectToDTO(updatedTraject);
        }
    }
}

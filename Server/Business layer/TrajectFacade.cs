using Business_layer.DTO;
using Business_layer.Interfaces;
using Business_layer.Interfaces.Mapping;
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
        private readonly ITrajectMapper _trajectMapper;

        public TrajectFacade(ITrajectRepository repositoryTraject,
                        ITrajectMapper trajectMapper)
        {
            _repositoryTraject = repositoryTraject;
            _trajectMapper = trajectMapper;
        }

        public List<string> GetTrajectTypes()
        {
            return _repositoryTraject.GetTrajectTypes();
        }

        public List<TrajectDTO> GetTrajecten(TrajectFilter filter)
        {
            return _repositoryTraject.GetTrajecten(filter)
                        .Select(traject => _trajectMapper.MapToDTO(traject))
                        .ToList();
        }

        public List<TrajectDTO> GetBuyableTrajecten(TrajectFilter filter)
        {
            return _repositoryTraject.GetBuyableTrajecten(filter)
                        .Select(traject => _trajectMapper.MapToDTO(traject))
                        .ToList();
        }

        public TrajectDTO GetTraject(int id)
        {
            var traject = _repositoryTraject.GetTrajectById(id);
            if (traject == null)
                return null;
            return _trajectMapper.MapToDTO(traject);
        }

        public TrajectDTO AddTraject(TrajectCreateUpdateDTO traject)
        {
            var newTraject = _trajectMapper.MapToModel(traject);
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
            return _trajectMapper.MapToDTO(createdTraject);
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
            return _trajectMapper.MapToDTO(deletedTraject);
        }

        public TrajectDTO UpdateTraject(TrajectCreateUpdateDTO traject, int id)
        {
            var newTraject = _trajectMapper.MapToModel(traject);
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
            return _trajectMapper.MapToDTO(updatedTraject);
        }
        public int GetAmountSold(int id)
        {
            var amountSold = _repositoryTraject.GetAmountSold(id);

            return amountSold;
        }
    }
}

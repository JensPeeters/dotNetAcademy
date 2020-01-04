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
    public class CursusFacade : ICursusFacade
    {
        private readonly ICursusRepository _repositoryCursus;
        private readonly ICursusMapper _cursusMapper;

        public CursusFacade(ICursusRepository repositoryCursus,
                            ICursusMapper cursusMapper)
        {
            _cursusMapper = cursusMapper;
            _repositoryCursus = repositoryCursus;
        }

        public List<string> GetCursusTypes()
        {
            return _repositoryCursus.GetCursusTypes();
        }

        public List<CursusDTO> GetCursussen(CursusFilter filter)
        {
            return _repositoryCursus.GetCursussen(filter)
                        .Select(cursus => _cursusMapper.MapToDTO(cursus))
                        .ToList();
        }

        public List<CursusDTO> GetBuyableCursussen(CursusFilter filter)
        {
            return _repositoryCursus.GetBuyableCursussen(filter)
                        .Select(cursus => _cursusMapper.MapToDTO(cursus))
                        .ToList();
        }

        public CursusDTO GetCursus(int id)
        {
            var cursus = _repositoryCursus.GetCursusById(id);
            if (cursus == null)
                return null;
            return _cursusMapper.MapToDTO(cursus);
        }

        public CursusDTO AddCursus(CursusCreateUpdateDTO cursus)
        {
            var newCursus = _cursusMapper.MapToModel(cursus);
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
            return _cursusMapper.MapToDTO(createdCursus);
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
            return _cursusMapper.MapToDTO(deletedCursus);
        }

        public CursusDTO UpdateCursus(CursusCreateUpdateDTO cursus, int id)
        {
            var newCursus = _cursusMapper.MapToModel(cursus);
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
            return _cursusMapper.MapToDTO(updatedCursus);
        }

        public int GetAmountSold(int id)
        {
            var amountSold = _repositoryCursus.GetAmountSold(id);

            return amountSold;
        }
    }
}

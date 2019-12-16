using Business_layer.DTO;
using Business_layer.Interfaces;
using Business_layer.Interfaces.Mapping;
using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
namespace Business_layer
{
    public class KlantFacade : IKlantFacade
    {
        private readonly IKlantRepository _repositoryKlant;
        private readonly IAdminFacade _facadeAdmin;
        private readonly IKlantMapper _klantMapper;

        public KlantFacade(IKlantRepository repositoryKlant, IAdminFacade facadeAdmin,
                        IKlantMapper klantMapper)
        {
            _repositoryKlant = repositoryKlant;
            _facadeAdmin = facadeAdmin;
            _klantMapper = klantMapper;
        }

        public KlantDTO CreateKlant(string klantId)
        {
            var klant = _repositoryKlant.GetKlantByID(klantId);
            if (klant != null)
                return null;
            var newKlant = _klantMapper.MapToModel(klantId);
            var createdKlant = _repositoryKlant.CreateKlant(newKlant);
            try
            {
                _repositoryKlant.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _klantMapper.MapToDTO(createdKlant);
        }

        public KlantDTO DeleteKlant(string klantId)
        {
            var deletedKlant = _repositoryKlant.DeleteKlant(klantId);
            if (deletedKlant == null)
                return null;
            try
            {
                _repositoryKlant.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _klantMapper.MapToDTO(deletedKlant);
        }

        public KlantDTO MakeKlantAdmin(string klantId)
        {
            var klant = _repositoryKlant.GetKlantByID(klantId);
            if (klant == null)
                return null;
            _repositoryKlant.DeleteKlant(klantId);

            _facadeAdmin.CreateAdmin(klantId);
            
            try
            {
                _repositoryKlant.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _klantMapper.MapToDTO(klant);
        }

        public KlantDTO GetKlant(string klantId)
        {
            var klant = _repositoryKlant.GetKlantByID(klantId);
            if (klant == null)
                return null;
            return _klantMapper.MapToDTO(klant);
        }
    }
}

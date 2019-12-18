using Business_layer.DTO;
using Business_layer.Interfaces;
using Business_layer.Interfaces.Mapping;
using Data_layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
namespace Business_layer
{
    public class KlantFacade : IKlantFacade
    {
        private readonly IKlantRepository _repositoryKlant;
        private readonly IAdminRepository _repositoryAdmin;
        private readonly IKlantMapper _klantMapper;
        private readonly IAdminMapper _adminMapper;

        public KlantFacade(IKlantRepository repositoryKlant, IAdminRepository repositoryAdmin,
                        IKlantMapper klantMapper, IAdminMapper adminMapper)
        {
            _repositoryKlant = repositoryKlant;
            _repositoryAdmin = repositoryAdmin;
            _klantMapper = klantMapper;
            _adminMapper = adminMapper;
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

        public AdminDTO MakeKlantAdmin(string klantId)
        {
            var klant = _repositoryKlant.GetKlantByID(klantId);
            if (klant == null)
                return null;
            _repositoryKlant.DeleteKlant(klantId);

            var admin = _repositoryAdmin.GetAdminByID(klantId);
            if (admin != null)
                return null;
            var newAdmin = _adminMapper.MapToModel(klantId);
            var createdAdmin = _repositoryAdmin.CreateAdmin(newAdmin);
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

            return _adminMapper.MapToDTO(createdAdmin);
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

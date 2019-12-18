using Business_layer.DTO;
using Business_layer.Interfaces;
using Business_layer.Interfaces.Mapping;
using Data_layer.Interfaces;
using Data_layer.Model;
using Data_layer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Business_layer
{
    public class AdminFacade : IAdminFacade
    {
        private readonly IAdminRepository _repositoryAdmin;
        private readonly IKlantRepository _repositoryKlant;
        private readonly IAdminMapper _adminMapper;
        private readonly IKlantMapper _klantMapper;

        public AdminFacade(IAdminRepository repositoryAdmin, IKlantRepository repositoryKlant,
                            IAdminMapper adminMapper, IKlantMapper klantMapper)
        {
            _repositoryAdmin = repositoryAdmin;
            _repositoryKlant = repositoryKlant;
            _adminMapper = adminMapper;
            _klantMapper = klantMapper;
        }

        public AdminDTO CreateAdmin(string adminId)
        {
            var admin = _repositoryAdmin.GetAdminByID(adminId);
            if (admin != null)
                return null;
            var newAdmin = _adminMapper.MapToModel(adminId);
            var createdAdmin = _repositoryAdmin.CreateAdmin(newAdmin);
            try
            {
                _repositoryAdmin.SaveChanges();
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

        public AdminDTO DeleteAdmin(string adminId)
        {
            var deletedAdmin = _repositoryAdmin.DeleteAdmin(adminId);
            if (deletedAdmin == null)
                return null;
            try
            {
                _repositoryAdmin.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _adminMapper.MapToDTO(deletedAdmin);
        }

        public KlantDTO MakeAdminKlant(string adminId)
        {
            var admin = _repositoryAdmin.GetAdminByID(adminId);
            if (admin == null)
                return null;
            _repositoryAdmin.DeleteAdmin(adminId);

            var klant = _repositoryKlant.GetKlantByID(adminId);
            if (klant != null)
                return null;
            var newKlant = _klantMapper.MapToModel(adminId);
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

        public AdminDTO GetAdmin(string adminId)
        {
            var admin = _repositoryAdmin.GetAdminByID(adminId);
            if (admin == null)
                return null;
            return _adminMapper.MapToDTO(admin);
        }
    }
}

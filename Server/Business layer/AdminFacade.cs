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
        private readonly IAdminMapper _adminMapper;

        public AdminFacade(IAdminRepository repositoryAdmin,
                            IAdminMapper adminMapper)
        {
            _repositoryAdmin = repositoryAdmin;
            _adminMapper = adminMapper;
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

        public AdminDTO DeleteAdmin(string AdminId)
        {
            var deletedAdmin = _repositoryAdmin.DeleteAdmin(AdminId);
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

        public AdminDTO GetAdmin(string adminId)
        {
            var admin = _repositoryAdmin.GetAdminByID(adminId);
            if (admin == null)
                return null;
            return _adminMapper.MapToDTO(admin);
        }
    }
}

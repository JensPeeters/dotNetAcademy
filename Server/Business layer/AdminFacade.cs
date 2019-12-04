using Business_layer.DTO;
using Business_layer.Interfaces;
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

        public AdminFacade(AdminRepository repositoryAdmin)
        {
            this._repositoryAdmin = repositoryAdmin;
        }

        private static Admin ConvertCreateUpdateDTOToAdmin(string adminId)
        {
            return new Admin()
            {
                AzureId = adminId
            };
        }
        private static AdminDTO ConvertAdminToDTO(Admin admin)
        {
            return new AdminDTO()
            {
                AzureId = admin.AzureId
            };
        }

        public AdminDTO CreateAdmin(string adminId)
        {
            var admin = _repositoryAdmin.GetAdminByID(adminId);
            if (admin != null)
                return null;
            var newAdmin = ConvertCreateUpdateDTOToAdmin(adminId);
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
            return ConvertAdminToDTO(createdAdmin);
        }

        public AdminDTO GetAdmin(string adminId)
        {
            var admin = _repositoryAdmin.GetAdminByID(adminId);
            if (admin == null)
                return null;
            return ConvertAdminToDTO(admin);
        }
    }
}

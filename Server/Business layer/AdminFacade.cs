using Business_layer.DTO;
using Data_layer.Model;
using Data_layer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer
{
    public class AdminFacade
    {
        private readonly AdminRepository repository;

        public AdminFacade(AdminRepository repository)
        {
            this.repository = repository;
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
            var admin = repository.GetAdminByID(adminId);
            if (admin != null)
                return null;
            var newAdmin = ConvertCreateUpdateDTOToAdmin(adminId);
            var createdAdmin = repository.CreateAdmin(newAdmin);
            return ConvertAdminToDTO(createdAdmin);
        }

        public AdminDTO GetAdmin(string adminId)
        {
            var admin = repository.GetAdminByID(adminId);
            if (admin == null)
                return null;
            return ConvertAdminToDTO(admin);
        }
    }
}

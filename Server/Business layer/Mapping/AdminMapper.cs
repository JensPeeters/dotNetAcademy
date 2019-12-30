using Business_layer.DTO;
using Business_layer.Interfaces.Mapping;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Mapping
{
    public class AdminMapper : IAdminMapper
    {
        public AdminDTO MapToDTO(Admin model)
        {
            return new AdminDTO()
            {
                AzureId = model.AzureId
            };
        }

        public Admin MapToModel(string adminId)
        {
            return new Admin()
            {
                AzureId = adminId
            };
        }
    }
}

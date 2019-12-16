using Business_layer.DTO;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Interfaces.Mapping
{
    public interface IAdminMapper
    {
        AdminDTO MapToDTO(Admin model);
        Admin MapToModel(string adminId);
    }
}

using Business_layer.DTO;

namespace Business_layer.Interfaces
{
    public interface IAdminFacade
    {
        AdminDTO CreateAdmin(string adminId);
        AdminDTO DeleteAdmin(string AdminId);
        AdminDTO GetAdmin(string adminId);
    }
}

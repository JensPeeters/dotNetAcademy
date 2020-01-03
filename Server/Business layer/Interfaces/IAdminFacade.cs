using Business_layer.DTO;

namespace Business_layer.Interfaces
{
    public interface IAdminFacade
    {
        AdminDTO CreateAdmin(string adminId);
        AdminDTO DeleteAdmin(string adminId);
        KlantDTO MakeAdminKlant(string adminId);
        AdminDTO GetAdmin(string adminId);
    }
}

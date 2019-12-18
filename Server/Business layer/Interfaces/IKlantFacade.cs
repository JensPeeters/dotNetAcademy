using Business_layer.DTO;

namespace Business_layer.Interfaces
{
    public interface IKlantFacade
    {
        KlantDTO CreateKlant(string klantId);
        KlantDTO DeleteKlant(string klantId);
        AdminDTO MakeKlantAdmin(string klantId);
        KlantDTO GetKlant(string klantId);
    }
}

using Business_layer.DTO;
using Data_layer.Filter.ProductenFilters;
using System.Collections.Generic;

namespace Business_layer.Interfaces
{
    public interface ICursusFacade
    {
        List<string> GetCursusTypes();

        List<CursusDTO> GetCursussen(CursusFilter filter);

        List<CursusDTO> GetBuyableCursussen(CursusFilter filter);

        CursusDTO GetCursus(int id);

        CursusDTO AddCursus(CursusCreateUpdateDTO cursus);

        CursusDTO DeleteCursus(int id);

        CursusDTO UpdateCursus(CursusCreateUpdateDTO cursus, int id);
    }
}

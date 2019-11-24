using Business_layer.DTO;
using Data_layer.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Interfaces
{
    public interface ICursusFacade
    {
        List<CursusDTO> GetCursussen(ProductFilterQuery filter);

        CursusDTO GetCursus(int id);

        CursusDTO AddCursus(CursusCreateUpdateDTO cursus);

        CursusDTO DeleteCursus(int id);

        CursusDTO UpdateCursus(CursusCreateUpdateDTO cursus, int id);
    }
}

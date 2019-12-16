using Business_layer.DTO;
using Data_layer.Filter.ProductenFilters;
using System.Collections.Generic;

namespace Business_layer.Interfaces
{
    public interface ITrajectFacade
    {
        List<TrajectDTO> GetTrajecten(TrajectFilter filter);
        List<TrajectDTO> GetBuyableTrajecten(TrajectFilter filter);
        TrajectDTO GetTraject(int id);

        TrajectDTO AddTraject(TrajectCreateUpdateDTO traject);

        TrajectDTO DeleteTraject(int id);

        TrajectDTO UpdateTraject(TrajectCreateUpdateDTO traject, int id);
    }
}

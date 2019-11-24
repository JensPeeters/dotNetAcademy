using Business_layer.DTO;
using Data_layer.Filter;
using Data_layer.Filter.ProductenFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Interfaces
{
    public interface ITrajectFacade
    {
        List<TrajectDTO> GetTrajecten(TrajectFilter filter);

        TrajectDTO GetTraject(int id);

        TrajectDTO AddTraject(TrajectCreateUpdateDTO traject);

        TrajectDTO DeleteTraject(int id);

        TrajectDTO UpdateTraject(TrajectCreateUpdateDTO traject, int id);
    }
}

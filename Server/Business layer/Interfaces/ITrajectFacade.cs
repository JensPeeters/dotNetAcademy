using Business_layer.DTO;
using Data_layer.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Interfaces
{
    public interface ITrajectFacade
    {
        List<TrajectDTO> GetTrajecten(ProductFilter filter);

        TrajectDTO GetTraject(int id);

        TrajectDTO AddTraject(TrajectCreateUpdateDTO traject);

        TrajectDTO DeleteTraject(int id);

        TrajectDTO UpdateTraject(TrajectCreateUpdateDTO traject, int id);
    }
}

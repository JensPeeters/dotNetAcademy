using Data_layer.Filter;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_layer.Interfaces
{
    public interface ITrajectRepository
    {
        List<Traject> GetTrajecten(ProductFilter filter);
        Traject GetTrajectByTitel(string titel);
        Traject GetTrajectById(int id);
        Traject AddTraject(Traject traject);
        Traject DeleteTraject(int id);
        Traject UpdateTraject(Traject traject);
    }
}

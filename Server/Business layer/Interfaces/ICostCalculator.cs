using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Interfaces
{
    public interface ICostCalculator
    {
        double CalculateCost(Winkelwagen winkelwagen);
    }
}

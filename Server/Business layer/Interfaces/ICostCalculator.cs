using Data_layer.Model;

namespace Business_layer.Interfaces
{
    public interface ICostCalculator
    {
        double CalculateCost(Winkelwagen winkelwagen);
    }
}

using Business_layer.Interfaces;
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer
{
    public class CostCalculator : ICostCalculator
    {
        public double CalculateCost(Winkelwagen winkelwagen)
        {
            double totaal = 0;
            foreach (WinkelwagenItem winkelwagenItem in winkelwagen.Producten)
            {
                totaal += winkelwagenItem.Product.Prijs * winkelwagenItem.Aantal;
            }
            return totaal;
        }
    }
}

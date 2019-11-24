using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.DTO
{
    class BestellingCreateUpdateDTO
    {
        public DateTime Datum { get; set; }
        public ICollection<BestellingItem> Producten { get; set; }
        public double TotaalPrijs { get; set; }
        public Klant Klant { get; set; }
    }
}

using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.DTO
{
    public class BestellingDTO
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public ICollection<BestellingItem> Producten { get; set; }
        public double TotaalPrijs { get; set; }
        public Klant Klant { get; set; }
    }
}

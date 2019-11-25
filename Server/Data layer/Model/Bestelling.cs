using System;
using System.Collections.Generic;

namespace Data_layer.Model
{
    public class Bestelling
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public ICollection<BestellingItem> Producten { get; set; }
        public double TotaalPrijs { get; set; }
        public Klant Klant { get; set; }
    }
}

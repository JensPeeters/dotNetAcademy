using System;
using System.Collections.Generic;
using System.Text;

namespace Data_layer.Model
{
    public class Bestelling
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public ICollection<Product> Producten { get; set; }
        public double TotaalPrijs { get; set; }
        public Klant Klant { get; set; }
    }
}

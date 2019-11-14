using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_layer.DTO
{
    public class WinkelwagenDTO
    {
        [Key]
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public Klant Klant { get; set; }

        public ICollection<WinkelwagenItem> Producten { get; set; }

        public double TotaalPrijs { get; set; }
    }
}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_layer.Model
{
    public class WinkelwagenItem
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }

        public int Aantal { get; set; }

        [JsonIgnore]
        public Winkelwagen Winkelwagen { get; set; }
    }
}

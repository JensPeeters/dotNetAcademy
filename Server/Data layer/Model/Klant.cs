using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_layer.Model
{
    public class Klant : User
    {
        [JsonIgnore]
        public ICollection<Winkelwagen> Winkelwagens { get; set; }
        [JsonIgnore]
        public ICollection<Bestelling> Bestellingen { get; set; }
    }
}

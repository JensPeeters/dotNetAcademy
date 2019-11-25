using Newtonsoft.Json;
using System.Collections.Generic;

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

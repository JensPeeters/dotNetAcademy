using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_layer.Model
{
    public class Klant
    {
        public int Id { get; set; }

        public string Naam { get; set; }


        [JsonIgnore]
        public ICollection<Winkelwagen> Winkelwagens { get; set; }
    }
}

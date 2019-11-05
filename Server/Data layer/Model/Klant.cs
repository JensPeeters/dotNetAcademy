using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_layer.Model
{
    public class Klant
    {
        [Key]
        public int Id { get; set; }
        public string AzureId { get; set; }
        [JsonIgnore]
        public ICollection<Winkelwagen> Winkelwagens { get; set; }
    }
}

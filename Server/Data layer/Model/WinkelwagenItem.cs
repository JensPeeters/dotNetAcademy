using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_layer.Model
{
    public class WinkelwagenItem : VerkoopItem
    {
        [JsonIgnore]
        public Winkelwagen Winkelwagen { get; set; }
    }
}

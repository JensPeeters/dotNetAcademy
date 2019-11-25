using Newtonsoft.Json;

namespace Data_layer.Model
{
    public class WinkelwagenItem : VerkoopItem
    {
        [JsonIgnore]
        public Winkelwagen Winkelwagen { get; set; }
    }
}

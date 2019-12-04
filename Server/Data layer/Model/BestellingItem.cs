using Newtonsoft.Json;

namespace Data_layer.Model
{
    public class BestellingItem : VerkoopItem
    {
        [JsonIgnore]
        public Bestelling Bestelling { get; set; }
    }
}

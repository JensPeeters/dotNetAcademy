using Data_layer.Model;
using System.Collections.Generic;

namespace Business_layer.DTO
{
    public class KlantDTO : UserDTO
    {
        public ICollection<Winkelwagen> Winkelwagens { get; set; }
        public ICollection<Bestelling> Bestellingen { get; set; }
    }
}

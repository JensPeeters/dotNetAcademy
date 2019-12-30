using System.Collections.Generic;

namespace Data_layer.Model
{
    public class Traject : Product

    {
        public Traject()
        {
            Categorie = "Traject";
        }
        
        public ICollection<Cursus> Cursussen { get; set; }
    }
}

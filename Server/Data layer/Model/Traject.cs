
using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data_layer.Model
{
    public class Traject : Product

    {
        public Traject()
        {
            this.Categorie = "Traject";
        }
        
        public ICollection<Cursus> Cursussen { get; set; }
    }
}

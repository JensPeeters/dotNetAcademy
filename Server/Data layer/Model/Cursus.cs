using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data_layer.Model
{
    public class Cursus : Product
    {
        public Cursus()
        {
            this.Categorie = "Cursus";
        }

    }
}

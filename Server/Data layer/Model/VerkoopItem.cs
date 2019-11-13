using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_layer.Model
{
    public abstract class VerkoopItem
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }

        public int Aantal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_layer.Model
{
    public abstract class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public double Prijs { get; set; }
        public string Categorie { get; set; }
        public string FotoURLCard { get; set; }
        public string Type { get; set; }
        [MaxLength(120)]
        public string Beschrijving { get; set; }
        public string LangeBeschrijving { get; set; }
        [Required]
        public string Titel { get; set; }
    }
}

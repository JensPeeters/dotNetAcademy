using System.ComponentModel.DataAnnotations;

namespace Business_layer.DTO
{
    public abstract class ProductDTO
    {
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

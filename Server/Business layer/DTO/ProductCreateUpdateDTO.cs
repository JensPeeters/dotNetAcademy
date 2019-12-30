using System.ComponentModel.DataAnnotations;

namespace Business_layer.DTO
{
    public class ProductCreateUpdateDTO
    {
        [Required]
        public double Prijs { get; set; }
        public string Categorie { get; set; }
        public string FotoURLCard { get; set; }
        public bool IsBuyable { get; set; }
        public string Type { get; set; }
        [MaxLength(120)]
        public string Beschrijving { get; set; }
        public string LangeBeschrijving { get; set; }
        [Required]
        public string Titel { get; set; }
        public int OrderNumber { get; set; }
    }
}

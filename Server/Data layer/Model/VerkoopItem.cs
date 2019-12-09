using System.ComponentModel.DataAnnotations;

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

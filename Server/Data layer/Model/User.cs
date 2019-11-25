using System.ComponentModel.DataAnnotations;

namespace Data_layer.Model
{
    public class User
    {
        [Key]
        public string AzureId { get; set; }
    }
}

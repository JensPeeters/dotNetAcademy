using System.ComponentModel.DataAnnotations;

namespace Business_layer.DTO
{
    public class UserDTO
    {
        [Key]
        public string AzureId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_layer.DTO
{
    public class UserDTO
    {
        [Key]
        public string AzureId { get; set; }
    }
}

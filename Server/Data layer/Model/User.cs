using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_layer.Model
{
    public class User
    {
        [Key]
        public string AzureId { get; set; }
    }
}

using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.DTO
{
    public class TrajectDTO : ProductDTO
    {
        
        public ICollection<Cursus> Cursussen { get; set; }
    }
}

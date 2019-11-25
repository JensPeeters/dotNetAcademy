using Data_layer.Model;
using System.Collections.Generic;

namespace Business_layer.DTO
{
    public class TrajectCreateUpdateDTO : ProductCreateUpdateDTO
    {

        public ICollection<Cursus> Cursussen { get; set; }
    }
}

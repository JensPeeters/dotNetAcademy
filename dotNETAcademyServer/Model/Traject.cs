using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNETAcademyServer.Model
{
    public class Traject
    {
        [Key]
        public int TrajectId { get; set; }
        [Required]
        public string Titel { get; set; }

        public string Type { get; set; }
        [MaxLength(120)]
        public string Beschrijving { get; set; }
        public string LangeBeschrijving { get; set; }
        public string FotoURLCard { get; set; }
        public ICollection<Cursus> Cursussen { get; set; }
        [Required]
        public double Prijs
        {
            get
            {
                double totaalPrijs = 0;
                foreach (Cursus cursus in Cursussen)
                {
                    totaalPrijs += cursus.Prijs;
                }
                return totaalPrijs;
            }
        }

       
    }
}

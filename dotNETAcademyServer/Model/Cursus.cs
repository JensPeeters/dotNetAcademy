using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNETAcademyServer.Model
{
    public class Cursus
    {
        [Key]
        public int CursusID { get; set; }
        [Required]
        public double Prijs { get; set; }
        public string Type { get; set; }
        [MaxLength(120)]
        public string Beschrijving { get; set; }
        [Required]
        public string Titel { get; set; }

    }
}

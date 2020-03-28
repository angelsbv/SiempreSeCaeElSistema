using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiempreSeCaeElSistema.Models
{
    public class Route
    {
        [Key]
        public int RtID { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string RtAirport { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string RtDestination { get; set; }
    }
}

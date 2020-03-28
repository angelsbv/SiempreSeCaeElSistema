using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiempreSeCaeElSistema.Models
{
    public class Aircraft
    {
        [Key]
        public int AcID { get; set; } //Ac => Aircraft

        [Column(TypeName = "varchar(45)")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string AcType { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int AcCapacity { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AcRegisterDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? AcModifiedDate { get; set; }
    }
}

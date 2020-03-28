using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiempreSeCaeElSistema.Models
{
    public partial class Employee
    {
        [Key]
        public int EmpID { get; set; }

        [Column(TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string EmpName { get; set; }

        [Column(TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string EmpLastName { get; set; }

        [Column(TypeName = "char(1)")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public char EmpGender { get; set; }

        [Column(TypeName = "varchar(max)")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string EmpHomeAdrs { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string EmpPhoneNumber { get; set; }

        [Column(TypeName = "varchar(320)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string EmpEmail { get; set; }

        [Column(TypeName = "Date")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime EmpBirthdate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime EmpHireDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? EmpModifiedDate { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string EmpCardID { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int EmpSalary { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string EmpType { get; set; }
    }
}

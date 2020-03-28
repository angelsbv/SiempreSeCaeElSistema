using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiempreSeCaeElSistema.Models
{
    public class PEmp
    {
        
    }

    [MetadataType(typeof(PEmp))]
    public partial class Employee
    {
        public string NombreCompleto { get { return $"{EmpName} {EmpLastName}"; } }
    }
}

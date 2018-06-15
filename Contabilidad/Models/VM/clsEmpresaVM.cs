using System;
using System.ComponentModel.DataAnnotations;

namespace Contabilidad.Models.VM
{
    public class clsEmpresaVM
    {
        [Key]
        public long EmpresaId { get; set; }

        [Display(Name = "Empresa")]
        public string EmpresaDes { get; set; }

        public string DataSource { get; set; }

        public string InitialCatalog { get; set; }
    }
}
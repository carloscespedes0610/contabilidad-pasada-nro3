﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsRegTipoPersonaVM
    {
        [Key]
        public long RegTipoPersonaId { get; set; }

        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        [Display(Name = "Tipo Persona")]
        public long TipoPersonaId { get; set; }

        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        [Display(Name = "Grupo del Plan")]
        public long PlanGrupoId { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _RegTipoPersonaId = nameof(RegTipoPersonaId);
        public static string _TipoPersonaId = nameof(TipoPersonaId);
        public static string _PlanGrupoId = nameof(PlanGrupoId);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_progra_4.Models
{
    public class PacientesModel
    {


        [Display(Name ="Codigo")]
        public int IdPacientes { get; set; }
        [Display(Name ="Nombre")]
        [Required(ErrorMessage = "Requerido")]
        public string NombrePaciente { get; set; }
        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "Requerido")]
        public string Cedula { get; set; }
        [Display(Name = "Tipo de sangre")]
        [Required(ErrorMessage = "Requerido")]
        public string Tipo_de_Sangre { get; set; }
        [Display(Name = "Numero de cita")]
        public int IdCita { get; set; }
        public bool Eliminado { get; set; }

    }
}
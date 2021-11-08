using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/*
 * Clase Metadatos para validar las entradas de Registrado
 * Equipo: Occidente
 */

namespace Proyecto_Inge_Bases_Web.Models
{
    public partial class RegistradoMetadata
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Correo { get; set; }

        //[RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,12}")]
        [Required]
        public string Contrasena { get; set; }

        public string Nombre { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }
    }
}
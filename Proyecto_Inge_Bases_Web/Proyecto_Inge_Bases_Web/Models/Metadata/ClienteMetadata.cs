using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/*
 * Clase Metadatos para validar las entradas de Cliente
 * Equipo: Occidente
 */

namespace Proyecto_Inge_Bases_Web.Models
{
    public partial class ClienteMetadata
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Correo { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> FechaNacimiento { get; set; }

        public Nullable<int> Canton { get; set; }

        public string DireccionExacta { get; set; }

        public byte[] FotoPerfil { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> FechaCierre { get; set; }

        public Nullable<byte> CalificacionPromedio { get; set; }

        public bool BloquearNotificaciones { get; set; }
    }
}
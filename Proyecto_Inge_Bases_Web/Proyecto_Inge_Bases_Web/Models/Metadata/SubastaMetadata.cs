using System;
using System.ComponentModel.DataAnnotations;



/**
         *  Función: Clase de Metadatos para validar las entradas. 
         * Autor : Neon
**/

namespace Proyecto_Inge_Bases_Web.Models
{
    public partial class SubastaMetadata
    {

        public int ProductoIDSubastado { get; set; }
        public string CorreoSubastador { get; set; }
        public System.DateTime FechaPublicado { get; set; }

        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        public double PrecioMinimo { get; set; }

        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        public System.DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        public System.DateTime FechaFin { get; set; }
        public Nullable<short> Calificacion { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
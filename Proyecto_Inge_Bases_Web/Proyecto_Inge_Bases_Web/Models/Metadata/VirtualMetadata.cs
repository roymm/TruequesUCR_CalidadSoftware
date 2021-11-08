using System;
using System.ComponentModel.DataAnnotations;



/**
         *  Función: Clase de Metadatos para validar las entradas. 
         * Autor : Neon
**/

namespace Proyecto_Inge_Bases_Web.Models
{
    public partial class VirtualMetadata
    {

        [Required(ErrorMessage = "*El producto a virtualizar es un campo obligatorio.")]
        public int ProductoID { get; set; }


        [Required(ErrorMessage = "*La cuenta asociada a a este producto es un campo obligatorio.")]
        public string CorreoCliente { get; set; }

        [Required(ErrorMessage = "*El tipo de archivo es un campo obligatorio.")]
        public string TipoDeArchivo { get; set; }

        [Required(ErrorMessage = "*Derechos de producto es un campo obligatorio.")]
        public string DerechosDeProducto { get; set; }

        [Required(ErrorMessage = "*Fuentes es un campo obligatorio.")]
        public string Fuentes { get; set; }

        public Nullable<System.DateTime> FechaExpiracion { get; set; }


        [Required(ErrorMessage = "*El producto a virtualizar es un campo obligatorio.")]
        public virtual Producto Producto { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Inge_Bases_Web.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("VirtualOOP")]
    public class VirtualOOP : ProductoOOP
    {
        // Recordemos que como se esta heredando, estos 2 atributos se estan trayendo de 
        // ProductoOOp

        //public int ProductoID { get; set; }
        //public string CorreoCliente { get; set; }
        public string TipoDeArchivo { get; set; }
        public string DerechosDeProducto { get; set; }
        public string Fuentes { get; set; }
        public Nullable<System.DateTime> FechaExpiracion { get; set; }

        //public virtual Producto Producto { get; set; }

        public override string FullIdentificacion 
        {

            get 
            {

                return Identificacion + " Soy Producto Virtual";

            }
        
        
        }

    }
}
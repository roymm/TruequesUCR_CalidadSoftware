using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Inge_Bases_Web.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("FisicoOOP")]
    public class FisicoOOP : ProductoOOP
    {

        // Recordemos que como se esta heredando, estos 2 atributos se estan trayendo de 
        // ProductoOOp

        //public int ProductoID { get; set; }
        //public string CorreoCliente { get; set; }
        //public virtual Producto Producto { get; set; }

        // falta los inserts globales // corregir transaccion

        public override string FullIdentificacion
        {

            get
            {

                return Identificacion + " Soy Producto Fisico";

            }


        }

    }
}
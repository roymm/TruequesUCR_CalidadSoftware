using Proyecto_Inge_Bases_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Proyecto_Inge_Bases_Web.ViewModels
{
    public class SubastaViewModel
    {
        public Producto Productos { get; set; }
        public Subasta Subastas { get; set; }
        public int ProductoIDSubastado { get; set; }
        public string CorreoSubastador { get; set; }
        public System.DateTime FechaPublicado { get; set; }

        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        public double PrecioMinimo { get; set; }

        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        public System.DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        public System.DateTime FechaFin { get; set; }

        public string FechFin { get; set; }
        public string FechInicio { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Mensaje { get; set; }
        public double PrecioOfrecido { get; set; }
        public Relacion_ClienteOfertaEnSubasta relacion_ClienteOfertaEnSubastas { get; set; }
        public List<SelectListItem> sublistas { get; set; }
    }
}
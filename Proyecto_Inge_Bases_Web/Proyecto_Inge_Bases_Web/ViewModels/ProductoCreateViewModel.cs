using Proyecto_Inge_Bases_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Inge_Bases_Web.ViewModels
{
    public class ProductoCreateViewModel
    {
        public Producto Producto { get; set; }
        public List<Categoria> Categoria { get; set; }
        public List<bool> selected { get; set; } // Esto va porque se pueden seleccionar multiples categorias
        public List<SelectListItem> sublistas { get; set; }

    }
}
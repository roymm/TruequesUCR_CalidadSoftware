using Proyecto_Inge_Bases_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Inge_Bases_Web.Views.TruequeViewModels
{
    //Para poder usar 2 modelos en la vita de notifications
    public class TruequeModels
    {
        public IEnumerable<Trueque> Trueques { get; set; }
        public IEnumerable<Producto> Productos { get;set; }
        public IEnumerable<Cliente> Clientes { get; set; }
    }
}
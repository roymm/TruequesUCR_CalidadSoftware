using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Inge_Bases_Web.Models
{
    public class MostrarListas
    {
       public List<VistaAmigo> Amigos { get; set; }
        public List<VistaNoAmigo> NoAmigos { get; set; }
        public List<PanelSublista> Sublistas { get; set; }
        public List<VistaEstaEnSublista> EstaEnSublistas { get; set; }
        public List<VistaEstaEnSublista> NoEsta { get; set; }
    }
}
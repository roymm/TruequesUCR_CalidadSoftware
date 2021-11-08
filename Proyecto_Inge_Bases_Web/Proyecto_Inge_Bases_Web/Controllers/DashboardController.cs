using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Proyecto_Inge_Bases_Web.Models;
using System.Data.Entity.Validation;

namespace Proyecto_Inge_Bases_Web.Controllers
{

    public class DashboardController : Controller
    {
        private TempPIEntities db = new TempPIEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            //Para verificar que el correo sea de un administrador.
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }

            var usuario = from u in db.Calificaciones_Cliente
                          select u;
            usuario = usuario.OrderBy(u => u.Nombre);

            
            return View(usuario.ToList());
        }

        public ActionResult EdadesClientes()
        {
            //Se encarga de la pagina del grafico de las edades de los clientes.

            //Para verificar que el correo sea de un administrador.
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }

            var usuario = from u in db.Calificaciones_Cliente
                          select u;
            usuario = usuario.OrderBy(u => u.Nombre);

            return View(usuario.ToList());
        }
        public ActionResult CalificacionesUsuarios()
        {
            //Se encarga de la pagina del grafico de las edades de los clientes.

            //Para verificar que el correo sea de un administrador.
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }

            var usuario = from u in db.Calificaciones_Cliente
                          select u;
            usuario = usuario.OrderBy(u => u.Nombre);

            return View(usuario.ToList());
        }

        public String CategoriaLabels(int raiz)
        {
            var categoria = db.Categorias.Where(cat => cat.CategoriaPadreID == raiz);
            categoria = categoria.OrderBy(u => u.Nombre);

            var ListaCategoriaNombre = categoria.Select(c => c.Nombre).ToList();
            String labelsOrdenados = string.Join(",", ListaCategoriaNombre.Select(x => string.Format("'{0}'", x))).Trim();


            return labelsOrdenados;

        }



        public int CantidadProductosCategoriaPadre(int id)
        {
            var categoria = db.Categorias.Find(id);
            var subcategorias = db.Categorias.Where(cat => cat.CategoriaPadreID == id);
            int cantidadProductos = categoria.Productoes.Count;
            foreach (var sc in subcategorias)
            {
                cantidadProductos = CantidadProductosCategoriaRec(sc, cantidadProductos);

            }

            return cantidadProductos;
        }

        private int CantidadProductosCategoriaRec(Categoria categoriaActual, int cantidad)
        {
            int cantidadProductos = cantidad + categoriaActual.Productoes.Count();
            var subcategorias = db.Categorias.Where(cat => cat.CategoriaPadreID == categoriaActual.CategoriaID);
            foreach (var sc in subcategorias)
            {
                cantidadProductos = CantidadProductosCategoriaRec(sc, cantidadProductos);
            }
            return cantidadProductos;
        }

        public String CantidadProductosCategoriaDatos(int raiz)
        {
            var categoria = db.Categorias.Where(cat => cat.CategoriaPadreID == raiz);
            categoria = categoria.OrderBy(u => u.Nombre);

            var ListaCantidadProductos = new List<int>();

            foreach (var cat in categoria)
            {
                ListaCantidadProductos.Add(CantidadProductosCategoriaPadre(cat.CategoriaID));
            }

            String datosOrdenados = string.Join(",", ListaCantidadProductos).Trim();


            return datosOrdenados;

        }



        public int CountSubastasEnCategoria(int id)
        {
            var subPorCat = from spc in db.subastasPorCategorias
                            group spc.ProductoIDSubastado by spc.IDCategoria into C
                            select new { IDCategoria = C.Key, SubastasCount = C.ToList().Count() };

            var ListaSubPorcat = subPorCat.ToList();
            var categoriaActual = ListaSubPorcat.Where(spc => spc.IDCategoria == id).FirstOrDefault();
            if(categoriaActual != null)
            {
                return categoriaActual.SubastasCount;
            }
            else
            {
                return 0;
            }
        }

        public int CantidadSubastasPorCategoriaPadre(int raiz)
        {
            var subcategorias = db.Categorias.Where(cat => cat.CategoriaPadreID == raiz);
            int cantidadSubastas = CountSubastasEnCategoria(raiz);
            foreach (var sc in subcategorias)
            {
                cantidadSubastas = CantidadSubastasPorCategoriaRec(sc.CategoriaID, cantidadSubastas);

            }

            return cantidadSubastas;
        }

        public int CantidadSubastasPorCategoriaRec(int categoriaIDActual, int cantidad)
        {
            int cantidadSubastas = cantidad + CountSubastasEnCategoria(categoriaIDActual);
            var subcategorias = db.Categorias.Where(cat => cat.CategoriaPadreID == categoriaIDActual);
            foreach (var sc in subcategorias)
            {
                cantidadSubastas = CantidadSubastasPorCategoriaRec(sc.CategoriaID, cantidadSubastas);
            }
            return cantidadSubastas;
        }

        public String CantidadSubastasCategoriaDatos(int raiz)
        {
            var categoria = db.Categorias.Where(cat => cat.CategoriaPadreID == raiz);
            categoria = categoria.OrderBy(u => u.Nombre);

            var ListaCantidadSubastas = new List<int>();

            foreach (var cat in categoria)
            {
                ListaCantidadSubastas.Add(CantidadSubastasPorCategoriaPadre(cat.CategoriaID));
            }

            String datosOrdenados = string.Join(",", ListaCantidadSubastas).Trim();


            return datosOrdenados;

        }



        public ActionResult VerIndex_ProductosDelDia()
        {
            return RedirectToAction("Index", "ProductosDelDia");
        }

        /* Acciones a Producto */
        public ActionResult Ver_Estadisticas()
        {
            return RedirectToAction("Index", "Estadisticas");
        }

        /*-------------------------*/

        /*  Metodos para Usuario */
        public ActionResult VerCategorias()
        {

            return RedirectToAction("Index", "Categorias");

        }

        /*  Copia descarada de lo de Jose xd */
        /*
        public String CantidadTruequesCategoriaDatos(int raiz)
        {
            var categoria = db.Categorias.Where(cat => cat.CategoriaPadreID == raiz);
            categoria = categoria.OrderBy(u => u.Nombre);

            var ListaCantidadTrueques = new List<int>();

            foreach (var cat in categoria)
            {
                ListaCantidadTrueques.Add(CantidadTruequesPorCategoriaPadre(cat.CategoriaID));
            }

            String datosOrdenados = string.Join(",", ListaCantidadTrueques).Trim();


            return datosOrdenados;

        }
        public int CantidadTruequesPorCategoriaPadre(int raiz)
        {
            var subcategorias = db.Categorias.Where(cat => cat.CategoriaPadreID == raiz);
            int cantidadTrueques = CountTruequesEnCategoria(raiz);
            foreach (var sc in subcategorias)
            {
                cantidadTrueques = CantidadTruequesPorCategoriaRec(sc.CategoriaID, cantidadTrueques);

            }

            return cantidadTrueques;
        }

        public int CountTruequesEnCategoria(int id)
        {
            var truePorCat = db.CategoriaConTruequeActivoes.Where(cat => cat.CategoriaID == id);
           
            int? truePorCatCant = truePorCat.Select(tpc=>tpc.CantidadAsociados).FirstOrDefault();
            if(truePorCatCant == null)
            {
                return 0;
            }
            else
            {
                return (int)truePorCatCant;
            }
        }
        public int CantidadTruequesPorCategoriaRec(int categoriaIDActual, int cantidad)
        {
            int cantidadTrueques = cantidad + CountTruequesEnCategoria(categoriaIDActual);
            var subcategorias = db.Categorias.Where(cat => cat.CategoriaPadreID == categoriaIDActual);
            foreach (var sc in subcategorias)
            {
                cantidadTrueques = CantidadTruequesPorCategoriaRec(sc.CategoriaID, cantidadTrueques);
            }
            return cantidadTrueques;
        }*/
    }


}
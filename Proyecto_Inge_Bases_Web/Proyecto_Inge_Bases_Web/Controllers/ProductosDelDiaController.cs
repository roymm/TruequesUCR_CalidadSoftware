using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Inge_Bases_Web.Models;
using System.Text.RegularExpressions;
using PagedList;

namespace Proyecto_Inge_Bases_Web.Controllers
{
    public class ProductosDelDiaController : Controller
    {

        private TempPIEntities db = new TempPIEntities();

        // GET: ProductosDelDia
        // Se inicia Actividad Supervisada - B78443
        public ActionResult Index(string searchString, int? page) {

            
            
            DateTime fechaHoy = DateTime.Today;

            var productos = from p in db.Productos_Info_Clientes
                            where DbFunctions.TruncateTime( p.FechaPublicado) == fechaHoy
                            select p;
            productos = productos.OrderBy(p => p.Nombre);


            // searchString = guarda la palabra que se desea encontrar dentro de la barra
            // page = pagina a mostrar
            // Encargado de filtrar la palabra ingresada con respecto al resto en el título

            // Se decide dejar la anterior version de la barra de busqueda por un problema en la verificacion de caracteres implementada en la AS, luego se arregla
            if (!String.IsNullOrEmpty(searchString))
            { 
               productos = productos.Where(p => p.Nombre.Contains(searchString));  
            }
            //Mostrar 9 productos por página
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            // Para cambiar el layout en el caso de que sea el administrador el que este viendo la pagina
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);
            var view = View("Index",productos.ToPagedList(pageNumber, pageSize));

            if (admin == null)
            {
                Cliente cliente = db.Clientes.Find(correo);
                if (cliente == null)
                {
                    return view;
                }
                view.MasterName = "~/Views/Shared/_LayoutCliente.cshtml";
                return view;
            }
            else 
            {
                view.MasterName = "~/Views/Shared/_LayoutAdministrador.cshtml";
                return view;
            }
            
        }

        // GET: ProductosDelDia/Details/5
        /*Ver los detalles de los productos*/
        public ActionResult Details(int? id, string correo)
        {

            string nombre_categoria = "";

            Producto prod = db.Productoes.Find(id, correo);
            
            if (prod != null) 
            { 
                foreach (var item in prod.Categorias) 
                {
                    nombre_categoria = item.Nombre;  
                }
            }


            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id, correo);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.esCliente = true;
            ViewBag.Categoria = nombre_categoria;
            string correo_user = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo_user);
            var view = View("Details", producto);

            if (admin == null)
            {
                Cliente cliente = db.Clientes.Find(correo_user);
                if (cliente == null)
                {
                    ViewBag.esCliente = false;
                    return view;
                }
                view.MasterName = "~/Views/Shared/_LayoutCliente.cshtml";
                return view;
            }
            else
            {
                view.MasterName = "~/Views/Shared/_LayoutAdministrador.cshtml";
                return view;
            }

        }

        /*Se llama al index de productos del día*/
        public ActionResult VerIndex_ProductosDelDia()
        {
            return RedirectToAction("Index", "ProductosDelDia");
        }

        /* Acciones a Producto */
        public ActionResult VerCreate_Producto()
        {
            return RedirectToAction("Create", "Producto");
        }

        /*-------------------------*/

        /*  Metodos para Usuario */
        public ActionResult VerSignUp_Usuario()
        {

            return RedirectToAction("IniciarSesion", "Registrado");

        }
        /* ----------------------*/


    }
}

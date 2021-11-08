using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Inge_Bases_Web.Models;
using PagedList;
using Microsoft.Ajax.Utilities;
using System.Data.SqlClient;
using System.Web.Services;
using Castle.Core.Internal;

namespace Proyecto_Inge_Bases_Web.Controllers
{
    public class OfertasController : Controller
    {

        private TempPIEntities db = new TempPIEntities();
        // GET: Ofertas
        public ActionResult Index(string searchBy, string currentFilter, string searchString, int? page)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual == "")
                return RedirectToAction("IniciarSesion", "Registrado");
            Desmarcar();
            //var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];


            ViewBag.CurrentSort = searchBy;
            ViewBag.NameSortParm = String.IsNullOrEmpty(searchBy) ? "Name asc" : "";
            ViewBag.SortPriceParm = searchBy == "Price" ? "Price desc" : "Price";


            //Esta condición se utiliza por  que el filtro se debe mantener siempre; cuando alguien realiza una busqueda mediante el searchbox 
            //este se debe reiniciar, por eso la condicion not null
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var products = db.Productoes.Include(c => c.Cliente);
            var productos = from p in db.Productoes
                            select p;

            // Para el borrado logico
            productos = productos.Where(p => p.Estado == true);

            // Si tiene correo ve los productos de el, al ingresar a detail debe de devolverse



            //Este if es el que se encarga de sacar los resultados que se vinculan a la busqueda realizada
            if (!String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => p.Nombre.Contains(searchString));
            }

            //Primero la busqueda y luego el ordenamiento, este switch va a permitir agregar distintas opciones de ordenamiento
            switch (searchBy)
            {
                case "Name asc":
                    productos = productos.OrderBy(p => p.Nombre);
                    break;

                case "Price desc":
                    productos = productos.OrderByDescending(p => p.PrecioEstimado);
                    break;
                case "Price":
                    productos = productos.OrderBy(p => p.PrecioEstimado);
                    break;
                default:
                    productos = productos.OrderBy(p => p.Nombre);
                    break;

            }

            //Esto convierte una sola pagina en una colección para realizar la paginacion
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(productos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id, string correo)
        {
            Desmarcar();
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            //esCliente se usa para el boton de denuncia
            ViewBag.esCliente = true;
            if (cookiecorreo == null)
            {
                ViewBag.esCliente = false;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id, correo);
            if (producto == null)
            {
                return HttpNotFound();
            }
            var productosVirtuales= from p in db.Virtuals
                                   select p;

            ViewBag.productosVirtuales = productosVirtuales;

            return View(producto);
        }

        public ActionResult MisProductosModal(string searchBy, string currentFilter, string searchString, int? page)
        {
            //var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            string correoActual = (string)HttpContext.Session["Correiro"];

            ViewBag.CurrentSort = searchBy;
            ViewBag.NameSortParm = String.IsNullOrEmpty(searchBy) ? "Name asc" : "";
            ViewBag.SortPriceParm = searchBy == "Price" ? "Price desc" : "Price";


            //Esta condición se utiliza por  que el filtro se debe mantener siempre; cuando alguien realiza una busqueda mediante el searchbox 
            //este se debe reiniciar, por eso la condicion not null

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var products = db.Productoes.Include(c => c.Cliente);
            var productos = from p in db.Productoes
                            select p;

            // Es para que me salgan solo mis productos, (yo siendo el usuario logueado)
            if (correoActual != null)
            {
                productos = productos.Where(p => p.CorreoCliente == correoActual);
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }

            //Este if es el que se encarga de sacar los resultados que se vinculan a la busqueda realizada
            if (!String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => p.Nombre.Contains(searchString));
            }

            //Primero la busqueda y luego el ordenamiento, este switch va a permitir agregar distintas opciones de ordenamiento
            switch (searchBy)
            {
                case "Name asc":
                    productos = productos.OrderBy(p => p.Nombre);
                    break;

                case "Price desc":
                    productos = productos.OrderByDescending(p => p.PrecioEstimado);
                    break;
                case "Price":
                    productos = productos.OrderBy(p => p.PrecioEstimado);
                    break;
                default:
                    productos = productos.OrderBy(p => p.Nombre);
                    break;
            }
            //Esto convierte una sola pagina en una colección para realizar la paginacion
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(productos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DetailsOferta(int? id, string correo)//int? id, string correo)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            Producto oferta = db.Productoes.Find(id, correo);
            var productos = from p in db.Productoes
                            select p;
            productos = productos.Where(p => p.Seleccionado == true && p.CorreoCliente == correoActual);
            if (productos.IsNullOrEmpty())
                ViewBag.Contiene = 0;
            else
                ViewBag.Contiene = 1;
            ViewBag.lista = productos;
            return View(oferta);
        }

        public ActionResult InsertarTrueque(int id, string correo)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            DateTime date = DateTime.Now;
            var productos = from p in db.Productoes
                            select p;
            productos = productos.Where(p => p.Seleccionado == true && p.CorreoCliente == correoActual);
            Trueque trueque = new Trueque();
            foreach (var item in productos)
            {
                //Corregir esto
                //db.spTrueque_InsertarOferta(id, correo, item.ProductoID, item.CorreoCliente, date);
               trueque.Inserte(id, correo, item.ProductoID, item.CorreoCliente, date);
            }
            //trueque.Inserte();
            return RedirectToAction("Index");
        }

        [WebMethod]
        public string Seleccionar(int id)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            Producto producto = db.Productoes.Find(id, correoActual);
            if (!producto.Seleccionado)
                producto.Seleccionado = true;
            else
                producto.Seleccionado = false;
            db.SaveChanges();
            return "listo";
        }

        [WebMethod]
        public string Desmarcar()
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            var productos = from p in db.Productoes
                            select p;
            productos = productos.Where(p => p.Seleccionado == true && p.CorreoCliente == correoActual);
            foreach (var item in productos)
                item.Seleccionado = false;
            db.SaveChanges();
            return "listo";
        }
    }
}
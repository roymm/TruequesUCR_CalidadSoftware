using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Integrador_Bases_Inge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            // Se cambio desde neon, estamos trabajando en el homepage
            // author; marco
            return RedirectToAction("Index", "ProductosDelDia");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult EditarPerfil()
        {
            return View();
        }

        public ActionResult Prueba1()
        {
            return View();
        }

        public ActionResult FormularioProducto()
        {
            ViewBag.Message = "Add a product.";

            return View();
        }

        public ActionResult ProductosDisponibles()
        {
            ViewBag.Message = "List of all available products.";

            return View();
        }

        public ActionResult MisProductos()
        {
            ViewBag.Message = "Your own products.";

            return View();
        }

        public ActionResult ListaUsuarios()
        {
            return View();
        }

        public ActionResult CondicionesUso()
        {
            return View();
        }

        /* Acciones a Usuario */
        public ActionResult VerRegistro_Registrado()
        {
            return RedirectToAction("Registro", "Registrado");
        }

        public ActionResult VerEditarPerfil_Cliente()
        {
            return RedirectToAction("EditarPerfil", "Cliente");
        }

        /* Acciones a Producto */
        public ActionResult VerCreate_Producto()
        {
            return RedirectToAction("Create", "Producto");  
        }

        /* -------------------------------*/

        /*  Metodos para ProductoDelDia */
        public ActionResult VerIndex_ProductosDelDia()
        {

            return RedirectToAction("Index", "ProductosDelDia");

        }
        /* ----------------------*/

        public ActionResult Error() {


            return View();
        }


    }
}
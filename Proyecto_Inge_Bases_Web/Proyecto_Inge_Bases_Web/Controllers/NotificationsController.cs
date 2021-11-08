using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Proyecto_Inge_Bases_Web.Models;
using PagedList;
using System.Runtime.Remoting.Messaging;
using Proyecto_Inge_Bases_Web.Views.TruequeViewModels;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace Proyecto_Inge_Bases_Web.Controllers
{
   
    public class NotificationsController : Controller
    {
        private TempPIEntities db = new TempPIEntities();        



        // GET: Trueque
        /**
            @Param: sortOrder. string que indica de que manera se va a ordenar la lista.
            @Param: currentFilter. Permite saber la configuracion de filtrado
            @Param: searchString. Recibe el string con lo que desea buscar.
            @Param: page. Permite saber cual es la pagina a mostrar
        */
       
        public ActionResult Recibe(string searchBy, string currentFilter, string searchString, int? page)
        {
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];

            //   HttpCookie cookiecorreo = Request.Cookies["users"];

            ViewBag.CurrentSort = searchBy;
            ViewBag.NameSortParm = String.IsNullOrEmpty(searchBy) ? "Name asc" : "";
            ViewBag.SortPriceParm = searchBy == "Price" ? "Price desc" : "Price";

           
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
            if (cookiecorreo != null)
            {
                productos = productos.Where(p => p.CorreoCliente == cookiecorreo.Value);
                productos = productos.Where(p => p.Estado == true);
            }
            else
            {
                // Arreglar esto return View("Index", "Home");
                //uno = 1;
                return View("Index", "Home");

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
     
        public ActionResult Solicitud(int p1)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            ViewBag.p1 = p1;
            ViewBag.correo = correoActual;

            int producto1 = p1;
            //Agarro los modelos para la vista con más de 1 modelo
            TruequeModels tmodel = new TruequeModels(); //Tiene la informacion del producto, y del trueque
            var trueques = db.Trueques.Include(t => t.Producto).Include(t => t.Producto1);
            var truequesB = from p in db.Trueques //Atributos relacionados a trueque
                            select p;
            var truequesC = from m in db.Trueques //Atributos relacionados a trueque
                            select m;


            if (correoActual != null) //Solo agarro los trueques donde yo soy publicador
            {
                truequesB = truequesB.Where(p => p.CorreoPublicador == correoActual && p.ProductoIDPublicador == p1);
            }
            else
            {
                // Arreglar esto return View("Index", "Home");
                return RedirectToAction("IniciarSesion", "Registrado");

            }

            tmodel.Trueques = truequesB.ToList();
            //Agarro productos
            var products = db.Productoes.Include(c => c.Cliente);
            var productos = from ps in db.Productoes //Para agarrar el nombre de los productos y la imagen
                            select ps;
            List<Producto> prod = new List<Producto>();
            foreach (var item in truequesB) { 
                foreach(var item2 in productos)
                    if (item.CorreoOfertante == item2.CorreoCliente && item.ProductoIDOfertante == item2.ProductoID)
                    {
                        prod.Add(item2);
                    }

            }


            tmodel.Productos = prod.ToList();

            //Agarro clientes 
            var clientes = db.Clientes.Include(c => c.Registrado);
            tmodel.Clientes = clientes.ToList();

            return View(tmodel);
        }

        // Se debe Borrar, solo se tiene empty por prueba
        public ActionResult FormularioEntrega() 
        {




            return View("FormularioEntrega");
        
        }


        public ActionResult DetallesProductosOfertados(int id, string correo, int p1, string correoOfertante)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            ViewBag.p1 = p1;
            ViewBag.correoOfertante = correoOfertante;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id, correo);
            if (producto.ProductoImagen1 == null)
            {
                byte[] image = new WebClient().DownloadData("https://i2.wp.com/www.bicifan.uy/wp-content/uploads/2016/09/producto-sin-imagen.png?fit=862%2C1104&ssl=1");
                var base64Image = Convert.ToBase64String(image);
                TempData["img"] = base64Image;
            }
            else
            {
                var base64Image = Convert.ToBase64String(producto.ProductoImagen1);
                TempData["img"] = base64Image;
            }

            if (producto == null)
            {
                return HttpNotFound();
            }

            var productosVirtuales = from p in db.Virtuals
                                     select p;

            ViewBag.productosVirtuales = productosVirtuales;


            return View(producto);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int p1,  string correo)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            
            TruequeModels tmodel = new TruequeModels(); //Tiene la informacion del producto, y del trueque
            
            var trueques = db.Trueques.Include(t => t.Producto).Include(t => t.Producto1);
            var truequesB = from p in db.Trueques //Atributos relacionados a trueque
                            select p;
            
            
            
            /*
            var productosA = from pA in db.Productoes
                            select pA;

            var productosB = from pB in db.Productoes
                             select pB ;*/

            

            if (correoActual != null) //Solo agarro los trueques donde yo soy publicador
            {
                truequesB = truequesB.Where(p => p.CorreoPublicador == correoActual && p.ProductoIDPublicador == p1 && p.CorreoOfertante == correo);
            }
            else
            {
                // Arreglar esto return View("Index", "Home");
                return RedirectToAction("IniciarSesion", "Registrado");
            }


            //truequesB.

            // OJO; SE TIENE QUE TENER CUIDADO, HAY QUE BUSCAQR EL ID EL SEGUNDO PRODUCTO PARA JALAR LOS NOMBRE DEL PRODUCTO
            // PREGUNTAR DONDE ES QUE SE TIENE ESE ID, O COMO SACARLO, LO COMENTADO SE CAE.



            var body1 = $"Truques@UCR informa: \n El usuario {correo} ha rechazado su oferta de truque que contiene los siguientes productos: \n *NULL \n *NULL";
            body1.Replace("\n", Environment.NewLine);
            var body2 = $"Truques@UCR informa: \n El usuario {correoActual} ha rechazado su oferta de truque que contiene los siguientes productos: \n *NULL \n *NULL";
            body2.Replace("\n", Environment.NewLine);
            
            EnviarCorreo(correoActual, body1);
            EnviarCorreo(correo, body2);

            //remueve todos los elementos
            db.Trueques.RemoveRange(truequesB);
            db.SaveChanges();
            return RedirectToAction("Recibe");
        }

 

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult MisTrueques()
        {
            string correoActual = (string)HttpContext.Session["Correiro"];

            //Agarro los modelos para la vista con más de 1 modelo
            TruequeModels tmodel = new TruequeModels(); //Tiene la informacion del producto, y del trueque
            var trueques = db.Trueques.Include(t => t.Producto).Include(t => t.Producto1);
            var truequesB = from p in db.Trueques //Atributos relacionados a trueque
                            select p;



            if (correoActual != null) //Solo agarro los trueques donde yo soy publicador
            {
                truequesB = truequesB.Where(p => p.CorreoPublicador == correoActual);
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Registrado"); // Me devuelve a registrarme
            }


            tmodel.Trueques = truequesB.ToList();
            //Agarro productos
            var products = db.Productoes.Include(c => c.Cliente);
            var productos = from ps in db.Productoes //Para agarrar el nombre de los productos y la imagen
                            select ps;


            tmodel.Productos = products.ToList();

            return View(tmodel);
        }

        /* Acciones a Usuario  */
        public ActionResult VerSignUp_Usuario()
        {
            return RedirectToAction("SignUp", "Usuario");
        }
        /* -------------------------------*/


        /*Estar monitoreando este metodo tambien, el asunto del correo esta hecho solo para notificaar cuendo se elimine el truque, mejorarlo con un SWITCH -> el subject, 
         * el body viene del metodo */
        [HttpPost]
        public ActionResult EnviarCorreo(string correo, string bodyMessage)
        {
            var fromAddress = new MailAddress("truequesecciucr@gmail.com", "Trueques UCR");
            var toAddress = new MailAddress(correo, "Estimado Usuario");
            const string fromPassword = "Admin.9876";
            const string subject = "Su oferta ha sido rechazada";
            string body = bodyMessage;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),

                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return View();
        }





    }
}

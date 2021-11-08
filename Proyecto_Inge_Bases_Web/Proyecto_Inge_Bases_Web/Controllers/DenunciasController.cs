using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.Web.Mvc;
using Proyecto_Inge_Bases_Web.Models;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Mail;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;

namespace Proyecto_Inge_Bases_Web.Controllers
{

    public class DenunciasController : Controller
    {
        private TempPIEntities db = new TempPIEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            //Para verificar que el correo sea de un administrador.
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);
            List<Registrado> registrados = db.Registradoes.ToList();
            //ViewBag.usuarios(registrados);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }

            List<Denuncia> denuncias = db.Denuncias.ToList();
            DateTime x = DateTime.Today;
            //int f = x.Year
            return View(denuncias);

        }
        public ActionResult Realizar_Denuncia(string correo, int productoID)
        {
            var usuario = db.Registradoes.Find(correo);
            var denuncia = new Denuncia();
            denuncia.Denunciado = usuario.Correo;
            denuncia.ProductoID = productoID;
            string correo2 = (string)HttpContext.Session["Correiro"];
            denuncia.Denunciante = correo2;
            DateTime fechaHoy = DateTime.Now;
            denuncia.Fecha = fechaHoy;
            ViewBag.Nombre = usuario.Nombre;
            return View(denuncia);
        }
        [HttpPost]
        public ActionResult Realizar_Denuncia([Bind(Include = "Fecha,Comentarios,Denunciante,Denunciado,Tipo, ProductoID")] Denuncia denuncias)
        {
            if (ModelState.IsValid)
            {
                db.Denuncias.Add(denuncias);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(denuncias);
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



        /*  Metodos para Usuario */
        public ActionResult VerCategorias()
        {

            return RedirectToAction("Index", "Categorias");

        }

        /*  Metodos para Usuario */
        public ActionResult DetallesProducto(int? id, string correo)
        {

            return RedirectToAction("Details", "ProductosDelDia", new { id = id, correo = correo });

        }

        public ActionResult EliminarDenuncia(DateTime fecha)
        {

            Denuncia adicional = db.Denuncias.Find(fecha);

            if (adicional != null)
            {
                ///*Mensaje para el usuario que hace la denuncia*/
                Registrado registrado = db.Registradoes.Find(adicional.Denunciante);

                string mensaje = registrado.Nombre + ", le agradecemos su interés en mantener segura la aplicación, sin embargo después de realizar un debido estudio, se ha determinado que la denuncia no es válida.";
                EnviarCorreo(adicional.Denunciante, mensaje, "Denuncia rechazada");

                db.Denuncias.Remove(adicional);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        /*Recibe el correo del usuario deunciado y el correo del usuario que realiza la denuncia*/
        public ActionResult BloquearUsuario(String denunciado, DateTime fecha)
        {
            /*Busca mediante el correo denunciado*/
            Registrado reg_denunciado = db.Registradoes.Find(denunciado);

            /*Se buscan los productos asociados al usuario denunciado*/
            var productos = from p in db.Productoes
                            where p.CorreoCliente == denunciado
                            select p;
            productos = productos.OrderBy(p => p.Nombre);

            /*Se buscan las denuncias asociadas al usuario denunciado*/
            var denuncias = from q in db.Denuncias
                            where q.Denunciado == denunciado
                            select q;
            denuncias = denuncias.OrderBy(q => q.Fecha);

            /*Se despublican los productos y se eliminan las denuncias asociadas al usuario denunciado*/
            foreach (var producto in productos)
            {
                producto.FechaPublicado = null;
                producto.Publicado = false;
            }

            
            if (reg_denunciado != null)
            {
                reg_denunciado.Bloqueo = true;
                

                ///*Se envian los correos respectivos al usuario bloqueado y al usuario que realiza la denuncia*/
                string mensaje_denuncia = "";
                Denuncia denuncia = db.Denuncias.Find(fecha);
                Producto producto = db.Productoes.Find(denuncia.ProductoID , denuncia.Denunciado);

                if (denuncia != null && producto != null) { 
                    switch (denuncia.Tipo)
                    {
                        case 0:
                            mensaje_denuncia = ", hemos recibido una denuncia en su contra debido a la publicación de un producto ilícito ( " +producto.Nombre+" )." ;
                            break;

                        case 1:
                            mensaje_denuncia = ", hemos recibido una denuncia en su contra debido al uso de lenguaje ofensivo.";
                            break;

                        case 2:
                            mensaje_denuncia = ", hemos recibido una denuncia en su contra debido a una descripción engañosa en uno de sus productos publicados ( " + producto.Nombre + " ).";
                            break;

                        case 3:
                            mensaje_denuncia = ", hemos recibido una denuncia en su contra debido a la publicación de un producto falso ( " + producto.Nombre + " ).";
                            break;

                        case 4:
                            mensaje_denuncia = ", hemos recibido una denuncia en su contra debido a una publicación que incita al odio.";
                            break;

                        case 5:
                            mensaje_denuncia = ", hemos recibido una denuncia en su contra.";
                            break;

                    }

                    string mensaje_Bloqueado = reg_denunciado.Nombre + mensaje_denuncia + " \nDespués de un debido analisis, se ha decidido prohibirle el acceso a la aplicación de forma permanentemente.";
                    EnviarCorreo(reg_denunciado.Correo, mensaje_Bloqueado, "Bloqueo de cuenta");

                    Registrado reg_denunciante = db.Registradoes.Find(denuncia.Denunciante);
                    string mensaje_denunciante = "Estimado usuario, le agradecemos por ayudarnos a mantener el orden y la seguridad en la aplicación. \nLe queremos informar que el usuario: " + reg_denunciado.Nombre+ " " +reg_denunciado.Apellido1+  ", ha sido bloqueado de forma permanente de la aplicación.";
                    EnviarCorreo(reg_denunciante.Correo, mensaje_denunciante, "Gracias por ayudarnos a mantener la aplicación segura");
                }

            }

            foreach (var denuncia in denuncias)
            {
                db.Denuncias.Remove(denuncia);
            }
            db.SaveChanges();


            return RedirectToAction("Index");
        }


        /*Metodo para enviar el correo al usuario bloqueado y denunciante*/
        [HttpPost]
        public void EnviarCorreo(string correo, string mensaje, string asunto)
        {
            var fromAddress = new MailAddress("truequesecciucr@gmail.com", "Trueques UCR");
            var toAddress = new MailAddress(correo, "Estimado Usuario");
            const string fromPassword = "Admin.9876";
            string subject = asunto;
            string body = mensaje;

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
        }
        public ActionResult EliminarProducto(int? idProducto, string correoDenunciado, string correoDenunciante, DateTime fecha)
        {

            Producto productoPorBorrar = db.Productoes.Find(idProducto,correoDenunciado);
            Denuncia denunciaPorBorrar = db.Denuncias.Find(fecha);
            string nombreProducto;
            if (productoPorBorrar != null || denunciaPorBorrar != null)
            {
                nombreProducto = productoPorBorrar.Nombre;
                db.Productoes.Remove(productoPorBorrar);
                db.Denuncias.Remove(denunciaPorBorrar);
                db.SaveChanges();

                ///*Mensaje para el usuario que hace la denuncia y el que es denunciado*/
                Registrado registrado = db.Registradoes.Find(correoDenunciante);
                string mensaje = registrado.Nombre + ", le agradecemos su interés en mantener segura la aplicación, el producto que usted denunció: \'" + nombreProducto + "\' ha sido borrado. ";
                EnviarCorreo(correoDenunciante, mensaje, "Gracias por su denuncia");

                registrado = db.Registradoes.Find(correoDenunciado);
                mensaje = registrado.Nombre + ", le informamos que su producto: \'" + nombreProducto + "\', ha sido borrado después de recibir una denuncia de otro usuario. ";
                EnviarCorreo(correoDenunciado, mensaje, "Producto eliminado");
            }

            return RedirectToAction("Index");
        }
        public ActionResult Suspension(string correo, byte tipo, DateTime fechaDenuncia)
        {
            TempData["FechaDefault"] = DateTime.Today.AddDays(21);
            
            Suspension suspensions = new Suspension();
            suspensions.Correo = correo;
            suspensions.TipoDenuncia = tipo;
            suspensions.FechaInicio = DateTime.Today;
            ViewBag.Correo = correo;
            return View(suspensions);

        }
        [HttpPost]
        public ActionResult Suspension([Bind(Include = "Correo,FechaInicio,FechaFin,Tipo")] Suspension suspension)
        {

            string correo = suspension.Correo;
            Suspension suspension2 = db.Suspensions.Find(correo);
            List<Denuncia> denuncia = new List<Denuncia>();
            foreach (var item in db.Denuncias)
            {
                if (item.Denunciado == correo) {
                    denuncia.Add(item);
                }
            }
            if(suspension2 != null)
            {
                suspension2.FechaInicio = suspension.FechaInicio;
                suspension2.FechaFin = suspension.FechaFin;
                suspension2.TipoDenuncia = suspension.TipoDenuncia;
            }
            else
            {
                db.Suspensions.Add(suspension);
            }
           
            db.SaveChanges();
            Denuncia denuncia1 = denuncia.First();
            ActualizarDenuncia(denuncia1.Fecha);

            /*Enviar correo al usuario suspendido*/
            
            string mensaje_suspension = "";
            Registrado registrado = db.Registradoes.Find(correo);
            Producto producto = db.Productoes.Find(denuncia1.ProductoID, denuncia1.Denunciado);

            if ((registrado != null) && (denuncia1 != null) && (producto != null)) { 
            
                /*En url va el nombre del controlador, el nombre del metodo que verifica el correo y el corre del usuario para pasarselo al metodo por parametro*/
                var url = "/TruequesG03/Apelaciones/Realizar_Apelacion?id=" + Url.Encode(correo);
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

                switch (denuncia1.Tipo - 10)
                {
                    case 0:
                        mensaje_suspension = "a la publicación de un producto ilícito ( " + producto.Nombre + " ).";
                        break;

                    case 1:
                        mensaje_suspension = "al uso de lenguaje ofensivo.";
                        break;

                    case 2:
                        mensaje_suspension = "a una descripción engañosa en uno de sus productos publicados ( " + producto.Nombre + " ).";
                        break;

                    case 3:
                        mensaje_suspension = "a la publicación de un producto falso ( " + producto.Nombre + " ).";
                        break;

                    case 4:
                        mensaje_suspension = "a una publicación que incita al odio.";
                        break;

                    case 5:
                        mensaje_suspension = "a una denuncia en su contra.";
                        break;

                }

                string mensaje = registrado.Nombre + ", le informamos que su cuenta ha sido suspendida debido " + mensaje_suspension + "\nEn el siguiente link podrá realizar una apelación: " + link;
                EnviarCorreo(correo, mensaje, "Cuenta suspendida");

            }
            return RedirectToAction("Index");
        }

        public ActionResult ActualizarDenuncia(DateTime fecha)
        {

            Denuncia adicional = db.Denuncias.Find(fecha);
            if (adicional != null) { 
                foreach (var item in db.Denuncias)
            {
                if (item.Denunciado == adicional.Denunciado && item.Tipo < 6)
                {
                    item.Tipo += 10;
                }
            }
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

    }
}
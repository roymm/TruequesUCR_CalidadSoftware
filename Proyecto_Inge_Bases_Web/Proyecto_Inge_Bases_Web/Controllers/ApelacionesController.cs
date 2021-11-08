using Proyecto_Inge_Bases_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace Proyecto_Inge_Bases_Web.Controllers
{
    public class ApelacionesController : Controller
    {
        private TempPIEntities db = new TempPIEntities();

        // GET: Apelaciones
        public ActionResult Index()
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }

            var apelacion = from a in db.Apelacions
                            select a;
            apelacion = apelacion.OrderBy(a => a.FechaApelacion);

            return View(apelacion.ToList());
        }

        [HttpPost]
        public ActionResult Realizar_Apelacion([Bind(Include = "Correo, FechaApelacion, FechaDenuncia, Comentario")] Apelacion apelacion)
        {
            if (ModelState.IsValid)
            {
                db.Apelacions.Add(apelacion);
                db.SaveChanges();
                return RedirectToAction("Apelacion_Exitosa");
            }

            return View(apelacion);
        }

        public ActionResult Apelacion_Exitosa()
        {
            return View("Apelacion_Exitosa");
        }

        public ActionResult Error_Apelacion()
        {
            return View("Error_Apelacion");
        }


        public ActionResult Realizar_Apelacion(string id)
        {
            var id2 = id.ToString();
            Registrado registrado = db.Registradoes.Find(id2);
            Suspension suspension = db.Suspensions.Find(id2);
            List<Denuncia> denuncia = new List<Denuncia>();

            foreach (var item in db.Denuncias)
            {
                if (item.Denunciado == id2 && item.Tipo >= 10) {
                    denuncia.Add(item);
                }
            }

            Denuncia denuncia1;
            if (denuncia.Any())
            {
                denuncia1 = denuncia.First();
            }
            else 
            {
                return View("Error_Apelacion");
            }

            Apelacion apelacion1 = new Apelacion();
            var apelacion = db.Apelacions.Find(id2);
            int contador = 0;

            foreach (var item in db.Suspensions)
            {
                if (id2 == item.Correo)
                {
                    contador++;

                }


            }
            if (registrado != null && denuncia1 != null && contador != 0 && apelacion == null )
            {
               
                apelacion1.Correo = id2;
                apelacion1.FechaApelacion = DateTime.Today;
                apelacion1.Comentario = " ";
                apelacion1.FechaDenuncia = denuncia1.Fecha;
                return View(apelacion1);
            }
            else
            {
                return View("Error_Apelacion");
            }

        }

        [HttpPost]
        public ActionResult Apelacion()
        {

            return View();
        }

        public ActionResult Aceptar_Apelacion(string denunciado) 
        {
            Suspension suspension = db.Suspensions.Find(denunciado);
            Apelacion apelacion = db.Apelacions.Find(denunciado);
            if (apelacion != null) 
            { 
                Denuncia denuncia = db.Denuncias.Find(apelacion.FechaDenuncia);
                if (suspension != null && denuncia != null) { 
                    db.Suspensions.Remove(suspension);
                    db.Apelacions.Remove(apelacion);
                    db.Denuncias.Remove(denuncia);
                    db.SaveChanges();
                    /*Mensaje para el usuario al que se le quita la suspension*/
                    Registrado registrado = db.Registradoes.Find(denunciado);
                    string mensaje = registrado.Nombre + ", le informamos que su apelación ha sido aceptada, ya puede iniciar sesión en la aplicación";
                    EnviarCorreo(denunciado, mensaje, "Apelación aceptada");
                }
            }
            

            return RedirectToAction("Index");
        }

        public ActionResult Rechazar_Apelacion(string denunciado)
        {
            Apelacion apelacion = db.Apelacions.Find(denunciado);
            if (apelacion != null) 
            { 
                Denuncia denuncia = db.Denuncias.Find(apelacion.FechaDenuncia);
                if (denuncia != null) { 
                    db.Apelacions.Remove(apelacion);
                    db.Denuncias.Remove(denuncia);
                    db.SaveChanges();            
                    /*Mensaje para el usuario al que se le rechaza la apelacion*/
                    Registrado registrado = db.Registradoes.Find(denunciado);

                    string mensaje = registrado.Nombre + ", le informamos que su apelación ha sido rechazada, su suspensión sigue activa";
                    EnviarCorreo(denunciado, mensaje, "Apelación rechazada");
                }
            }
            

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

        // GET: Apelaciones/Details/5
        public ActionResult DetallesProducto(int id, string correo)
        {
            return RedirectToAction("Details", "ProductosDelDia", new { id = id, correo = correo });
        }

        
    }
}

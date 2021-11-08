using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using Proyecto_Inge_Bases_Web.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Proyecto_Inge_Bases_Web.Controllers
{
    public class RegistradoController : Controller
    {
        private TempPIEntities db = new TempPIEntities();

        // GET: Registrado
        public ActionResult Index()
        {
            return View(db.Registradoes.ToList());
        }

        // GET: Registrado/Details/5
        public ActionResult Details(string Correo)
        {
            if (Correo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registrado registrado = db.Registradoes.Find(Correo);
            if (registrado == null)
            {
                return HttpNotFound();
            }
            return View(registrado);
        }

        // GET: Registrado/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }
        /*Inicio actividad supervisada Cesar Vasquez Q.
          Validaciones controlan excepciones y verifican que no se inserte condigo que no sea tipo correo
          Ademas valida que la contraseña cumpla con los requisitos establecidos
          No deja pasar nada que no cumpla con los requisitos de vallidación*/

        /*
         * Inicio AS 17/06/20 Mario Vargas Campos B67454 y Cesar Vasquez Q.
         * Historia ID: 1.2
         * Se implementaron pruebas enfoncadas en vulnerabilidades relacionadas con la inyeccion de codigo
         * SQL. La entrada de datos se manejan mediante forms los cuales manejan el formato de datos, 
         * para que el programa no permita parametros que no cumplen con las caracteristicas requeridas.
         */
    
         public ActionResult ValidarRegistro(FormCollection form)
        {
            string correo = form["email"].ToString();
            string Contraseña = form["Contraseña"].ToString();
            ScryptEncoder encoder = new ScryptEncoder();

            Registrado usuario = db.Registradoes.Find(correo);
            /*
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: Contraseña,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            */
            string ContraseñaHashed = encoder.Encode(Contraseña);

            string codigoEnlace = Guid.NewGuid().ToString();
            var enlaceVerificacion = "~/Cliente/EditarPerfilRegistrandose";
            //var enlace = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, codigoEnlace);

            if (usuario != null)
            {
                TempData["ImportError"] = "La cuenta con el correo electrónico asociado ya existe ";
                return RedirectToAction("Registro");
            }
            else
            {

                Registrado usuario1 = new Registrado();
                usuario1.Correo = correo;
                //Guarda contraseña hashed.
                usuario1.Contrasena = ContraseñaHashed;
                //usuario1.Contrasena = Contraseña;
                db.Registradoes.Add(usuario1);
                Cliente cliente1 = new Cliente();
                cliente1.Correo = usuario1.Correo;
                db.Clientes.Add(cliente1);
                db.SaveChanges();
                Session["UserName2"] = usuario1.Correo;//Se usa para la validacion del correo.


                //var enlaceSeguro = UserManager.GenerateEmailConfirmationTokenAsync(usuario1.Correo);

                /*
                var resultado = await UserManager.CreateAsync(usuario1.Correo, usuario1.Contrasena);
                if (resultado.Succeeded)
                {
                    var enlaceSeguro = await UserManager.GenerateEmailConfirmationTokenAsync(usuario1.Correo);
                    var callbackUrl = Url.Action(
                       "ConfirmEmail", "Account",
                       new { userId = usuario1.Correo, code = enlaceSeguro },
                       protocol: Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id,
                       "Correo de COnfrimacion",
                       "Porfavor confrime su cuenta dando click en el siguiente enlace: <a href=\""
                                                       + callbackUrl + "\">link</a>");
                    // ViewBag.Link = callbackUrl;   // Used only for initial demo.
                    return View("RegistroExitoso");
                }
            }
            */
                EnviarCorreo(correo, "¡Estimado Usuario! Gracias por registrarse en Trueques UCR.Por favor ingrese en el siguiente enlace: " + enlaceVerificacion + " para terminar de completar su perfil.");
                //EnviarCorreo(correo, "¡Hola Estimado Usuario! Gracias por registrarse en Trueques UCR.Por favor ingrese en el siguiente enlace: https://localhost:44324/Cliente/EditarPerfilRegistrandose para terminar de completar su perfil.");
                //usuario1.EstadoCuenta = true;   // cuanta activa.
                return View("RegistroExitoso");
            }
        }

        // Metdoo para enviar correo de verificacion.
        // 
        // Metdoo para enviar correo de verificacion.
        // 
        [HttpPost]
        public ActionResult EnviarCorreo(string correo, string bodyMessage)
        {
            
                var fromAddress = new MailAddress("imapdemoecci2020@gmail.com", "Trueques UCR");
                var toAddress = new MailAddress(correo, "Estimado Usuario");
                const string fromPassword = "Ab654321!";
                const string subject = "Verificacion de Identidad";
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

        //Metodo para verificar si un usuario puede recibir correos o no
        public Boolean verificarPrefNotificacion(string correo)
        {
            Boolean resultado = false;
            Registrado registrado = db.Registradoes.Find(correo);
            if (registrado.EstadoCuenta == false)
            { /* Se valida que el cliente tenga la cuenta desactivada, esto significa que aun no se ha activado
                                                    el perfil por primera vez y DEBE recibir el correo de verificacion */
                resultado = true;
            }
            else
            {
                Cliente cliente = db.Clientes.Find(correo);
                if (cliente.BloquearNotificaciones == false)
                {   /* Se valida que el cliente tenga desactivada la opcion de bloqueo de notificaciones */
                    resultado = true;
                }
            }
            return resultado;
        }


        //ActionResult de la vista de registro exitoso
        public ActionResult RegistroExitoso()
        {
            return View();
        }

        public ActionResult CondicionesUso()
        {
            return View();
        }
        public ActionResult PerfilCompleto()
        {
            return View();
        }

        public ActionResult IniciarSesion()
        {
            LogOut();//Para matar la cookie

            return View();
        }

        [HttpPost]
        public ActionResult ValidarInicioSesion(FormCollection form)
        {
            //var cliente = DependencyResolver.Current.GetService<ClienteController>();
            // ClienteController cliente = new ClienteController();
            //string correo1 = Request["correo1"].ToString();
            //string Contraseña1 = Request["Contraseña"].ToString();
            ScryptEncoder decoder = new ScryptEncoder();
            string correo1 = form["correo1"].ToString();
            string Contraseña1 = form["Contraseña1"].ToString();
            Registrado usuario = db.Registradoes.Find(correo1);
            Cliente cliente = db.Clientes.Find(correo1);
            Suspension suspension = db.Suspensions.Find(correo1);
            
            // guarda booleano en varialble "contraseñaValida"

            bool contraseñaValida;
            //Checkea que la contraseña ingresada sea igual a la que existe hashed en la Base de Datos.
            //contraseñaValida = decoder.Compare(Contraseña1, usuario.Contrasena);

            /*Supervisada 06 / 10 / 2020 OC - 2.6 Johan Murillo -César Vásquez*/

            string checkResp1 = form["checkbox5"]; //lee el valor del checkbox
            bool checkRespB1 = Convert.ToBoolean(checkResp1);


            if (checkRespB1 == true)
            {
                if (usuario.EstadoCuenta == false)
                {
                    //Proceso de Registro
                    if (usuario.Nombre == null)
                    {
                        return RedirectToAction("ReactivarProcRegistro");
                    }

                }
                else
                {
                    /*   //Cuenta activa
                    if (usuario.Correo != suspension.Correo)
                    {
                        return RedirectToAction("ReactivarCuentaAct");
                    }*/

                    //Bloqueo Permanente
                    if ((usuario.Bloqueo == true))
                    {
                        return RedirectToAction("ReactivarBloqPermanente");
                    }

                    //Suspension Temporal
                    if (usuario.Correo == suspension.Correo)
                    {
                        string mensajeReactivacion = "El usuario " + correo1 + " ha solicitado reactivar su cuenta. " + "El usuario ha estado inactivo desde " + cliente.FechaCierre + ". Favor de realizar el análisis del caso y notificar el resultado al usuario.";
                        //EnviarCorreo("rialfaro98@gmail.com", mensajeReactivacion);
                        EnviarCorreo("joalva18@gmail.com", mensajeReactivacion);
                        return RedirectToAction("ReactivarCuenta");
                    }
                }
            }


            if (usuario == null)
            {
                TempData["ImportError"] = "El correo o la contraseña son inválidos";
                return RedirectToAction("IniciarSesion");
            }
            //pregunta si la contraseña es valida.
            else if ((contraseñaValida = decoder.Compare(Contraseña1, usuario.Contrasena)) == false)

            //else if (Contraseña1  != usuario.Contrasena)
            {
                TempData["ImportError"] = "El correo o la contraseña son inválidos";
                return RedirectToAction("IniciarSesion");
            }
            else if (usuario.EstadoCuenta == false)
            {
                TempData["ImportError"] = "La cuenta no es válida, por favor verifique su correo";
                return RedirectToAction("IniciarSesion");
            }
            else if (usuario.Bloqueo == true)
            {
                TempData["ImportError"] = "La cuenta ha sido bloqueada.";
                return RedirectToAction("IniciarSesion");
            }
            else if (suspension!= null)
            {
                if (suspension.FechaFin>DateTime.Today) {
                    TempData["ImportError"] = "La cuenta ha sido suspendida. Verifique su correo para más detalles";
                    return RedirectToAction("IniciarSesion");
                }
                Login(usuario);
                if (usuario.Nombre == null)
                {

                    return RedirectToAction("EditarPerfil", "Cliente");
                }
                else
                {
                    return RedirectToAction("PerfilCliente", "Cliente");
                }
            }

            else
            {
                Login(usuario);
                Administrador admin = db.Administradors.Find(correo1);
                if (admin != null)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (usuario.Nombre == null)
                {

                    return RedirectToAction("EditarPerfil", "Cliente");
                }
                else
                {
                    return RedirectToAction("PerfilCliente", "Cliente");
                }
            }// agregar diferenciación pal adminy cliente normal
        }


        public string Login(Registrado r)
        {
            HttpCookie UserCookie = new HttpCookie("user", r.Correo.ToString());
            UserCookie.Expires.AddDays(1);
            HttpContext.Response.SetCookie(UserCookie);
            HttpCookie NewCookie = Request.Cookies["user"];
            Registrado usuario = db.Registradoes.Find(NewCookie.Value);
            Session["UserName"] = usuario.Nombre;
            Session["Correiro"] = usuario.Correo;
            return NewCookie.Value;
        }
        // Mata la Cookie
        public void LogOut()
        {
            Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            Session["UserName"] = "";
            Session["Correiro"] = "";

        }
        // POST: Registrado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Correo,Contrasena,Nombre,Apellido1,Apellido2,RegistradoID")] Registrado registrado)
        {
            if (ModelState.IsValid)
            {
                db.Registradoes.Add(registrado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registrado);
        }

        // GET: Registrado/Edit/5
        public ActionResult Edit(string Correo)
        {

            if (Correo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registrado registrado = db.Registradoes.Find(Correo);
            if (registrado == null)
            {
                return HttpNotFound();
            }

            return View(registrado);
        }

        //Agregado action result para verificar existenciqa de correo en la base de tados (Cesar V)
        //public ActionResult RestaurarContraseña(string correo) 
        //{
        //    if (correo == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Registrado registrado = db.Registradoes.Find(correo);
        //    if (registrado == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View();
        //}

        //Ligado al boton para cambiar contraseña, solamente muestra la página
        public ActionResult RestaurarContraseña()
        {
            return View();
        }

        //En está se envía el correo y se muestra un mensaje de que se envió el correo
        [HttpPost]
        public ActionResult RestaurarContraseña(FormCollection form)
        {
            string correo = form["correo1"].ToString();
            string codigoCambio = Guid.NewGuid().ToString();
            string mensaje = "";
            Registrado registrado = db.Registradoes.Find(correo);
            if (registrado != null)
            {
                var verifyUrl = "/Registrado/CambiarContrasena/" + codigoCambio;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                EnviarCorreo(correo, "Estimado usuario, recibimos una solicitud de cambio de contraseña, en el siguiente link podrá cambiar su contraseña. Si usted no solicito un cambio de contraseña ignore este correo. " + link);

                //Se almacena el codigo de cambio en la tupla del usuario registrado que solicitó el cambio de contraseña.
                registrado.CodigoCambioContrasena = codigoCambio;
                db.SaveChanges();

                mensaje = "Se ha enviado a su dirección de correo electrónico un enlace con el cual podrá cambiar su contraseña";
            }
            else
            {
                mensaje = "El correo no está asociado a ninguna cuenta";
            }
            ViewBag.Message = mensaje;
            return View();
        }

        //Recibe el codigo para cambiar la contrasena enviado en el link usuario.
        public ActionResult CambiarContrasena(string id)
        {
            //Verificar el codigo de cambio de contraseña
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            //Se busca al usuario con el codigo de cambio de contrasena que corresponda
            var usuario = db.Registradoes.Where(u => u.CodigoCambioContrasena == id).FirstOrDefault();
            if (usuario != null)
            {
                CambiarContrasenaModel model = new CambiarContrasenaModel();
                model.CodigoCambio = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CambiarContrasena(CambiarContrasenaModel model)
        {
            string mensaje = "";
            //Verifica que no hayan errores en los datos que se ingresaron a la hora de cambiar la contrasena
            if (ModelState.IsValid)
            {
                var usuario = db.Registradoes.Where(u => u.CodigoCambioContrasena == model.CodigoCambio).FirstOrDefault();
                if (usuario != null)
                {
                    ScryptEncoder encoder = new ScryptEncoder();
                    string ContrasenaHashed = encoder.Encode(model.NuevaContrasena);
                    usuario.Contrasena = ContrasenaHashed;
                    usuario.CodigoCambioContrasena = "";
                    db.SaveChanges();
                    return View("Cambio_Contrasena_Exitoso");
                }
            }
            mensaje = "No se puede actualizar";
            ViewBag.Messsage = mensaje;
            return View(model);   
        }

        public ActionResult Cambio_Contrasena_Exitoso() 
        {
            return View("CambioFueExitoso");
        }

        // POST: Registrado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Correo,Contrasena,Nombre,Apellido1,Apellido2,RegistradoID")] Registrado registrado)
        {
            if (ModelState.IsValid)
            {

                db.Entry(registrado).State = EntityState.Modified;
                registrado.EstadoCuenta = true;//Setea la cuenta en válida despues de editar
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registrado);
        }

        // GET: Registrado/Delete/5
        public ActionResult Delete(string Correo)
        {
            if (Correo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registrado registrado = db.Registradoes.Find(Correo);
            if (registrado == null)
            {
                return HttpNotFound();
            }
            return View(registrado);
        }

        // POST: Registrado/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Correo)
        {
            Registrado registrado = db.Registradoes.Find(Correo);
            db.Registradoes.Remove(registrado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReactivarCuenta()
        {
            return View("ReactivarCuenta");
        }

        public ActionResult ReactivarProcRegistro()
        {
            return View("ReactivarProcRegistro");
        }

        public ActionResult ReactivarBloqPermanente()
        {
            return View("ReactivarBloqPermanente");
        }

        public ActionResult ReactivarCuentaAct()
        {
            return View("ReactivarCuentaAct");
        }

        public ActionResult ReactivarCuentaInexistente()
        {
            return View("ReactivarCuentaInexistente");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

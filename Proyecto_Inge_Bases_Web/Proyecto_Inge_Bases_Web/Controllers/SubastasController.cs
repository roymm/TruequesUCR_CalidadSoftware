using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Globalization;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Services;
using Proyecto_Inge_Bases_Web.Models;
using PagedList;
using Microsoft.Ajax.Utilities;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Data.Entity.Migrations;
using Proyecto_Inge_Bases_Web.ViewModels;
using System.Windows.Forms;

namespace Proyecto_Inge_Bases_Web.Controllers
{
    public class SubastasController : Controller
    {
        private TempPIEntities db = new TempPIEntities();

        // GET: Subastas2
        public ActionResult Index()
        {
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            string correoActual = (string)HttpContext.Session["Correiro"];
            var subastas = db.sp_Subasta_Select(correoActual);
            List<int> visualizaciones = new List<int>();
            foreach (var item in subastas)
            {
                if (item.PublicoEspecial == 0)
                    visualizaciones.Add(-1);
                if (item.PublicoEspecial == 1)
                    visualizaciones.Add(db.spSubasta_ContarAmigos(item.ProductoIDSubastado, item.CorreoSubastador, item.FechaPublicado).Count());
                if (item.PublicoEspecial == 2)
                    visualizaciones.Add(-1);

                // visualizaciones.Add(db.spSubasta_ContarSublistas(item.ProductoIDSubastado, item.CorreoSubastador, item.FechaPublicado).Count());
            }
            ViewBag.visualizaciones = visualizaciones;
            
            subastas = db.sp_Subasta_Select(correoActual);

            if (correoActual != null) //Esto se supone que saca la información de la cookie del correo 
                return View(subastas.ToList());
            else
                return RedirectToAction("IniciarSesion", "Registrado");
            //return View(subastas.ToList());
        }

        // GET: Subastas2/Details/5
        public ActionResult Details(int? idProducto, string correo, DateTime? fecha) //probar dejando datetime en lugar de string
        {
            
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            if (idProducto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ponerlo viewbag el nombre del que gano la oferta
            Subasta subasta = db.Subastas.Find(idProducto, correo, fecha);
            if (subasta == null)
            {
                return RedirectToAction("RecursoNoEncontrado", "Subastas");
            }
            Relacion_ClienteOfertaEnSubasta ofertaMayor;// = new Relacion_ClienteOfertaEnSubasta();
            var oferta = from p in db.Relacion_ClienteOfertaEnSubasta
                         select p;
            var oferta2 = from m in db.Relacion_ClienteOfertaEnSubasta
                          select m;
            oferta = oferta.Where(p => p.ProductoIDSubastado == idProducto);
            int dif = DateTime.Compare(subasta.FechaFin, DateTime.Now);
            //double? maximo = oferta.Max(o => o.PrecioOfrecido);

            if (dif < 0) //Para confirmar si la subasta ya acabo
            {
                if (oferta.Any())
                {
                    double pre = oferta.Max(o => o.PrecioOfrecido);
                    oferta2 = oferta2.Where(m => m.PrecioOfrecido == pre && m.ProductoIDSubastado == idProducto);
                    ofertaMayor = db.Relacion_ClienteOfertaEnSubasta.SingleOrDefault(x => x.ProductoIDSubastado == idProducto && x.PrecioOfrecido == pre);
                    ViewBag.OfertaMaxima = pre;
                    ViewBag.Persona = ofertaMayor.CorreoOfertador;
                    //ofertaMayor = oferta.Max();
                    //ViewBag.Persona = ofertaMayor.CorreoOfertador;
                }
                else
                {
                    ViewBag.Persona = "No hubo ganadores";
                    ViewBag.OfertaMaxima = 0;
                }
            }


            
            return View(subasta);
        }


        // GET: Subastas2/Create

        public ActionResult Create(int? idProducto, string correo)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            Subasta subasta = new Subasta();
            Producto prod = new Producto();
            Producto pro = db.Productoes.Find(idProducto, correo);
            if (pro == null)
            {
                return RedirectToAction("RecursoNoEncontrado", "Subastas");
            }
            subasta.ProductoIDSubastado = (int)idProducto;
            subasta.CorreoSubastador = correo;
            //busque primero el producto que contenga ese id y luego lo guarde y se lo pase al modelo
            SubastaViewModel tmodel = new SubastaViewModel();
            var subastaB = from p in db.Subastas
                           select p;
            var productoB = from ps in db.Productoes
                            select ps;
            
            //filtro las listas correctas
            var sublistas = from s in db.Sublistas
                            select s;
            sublistas = sublistas.Where(s => s.CorreoDueno == correoActual);
            //meto en el selectList para mostrar las listas correctas en el view
            tmodel.sublistas = new List<SelectListItem>();
            tmodel.sublistas.Add(new SelectListItem { Text = "0", Value = "Público" });
            tmodel.sublistas.Add(new SelectListItem { Text = "0", Value = "Mis Amigos" });
            foreach (var item in sublistas) {
                tmodel.sublistas.Add(new SelectListItem { Text = item.id.ToString(), Value = item.NombreSublista });
            }

            tmodel.Productos = pro;
            tmodel.Subastas = subasta;
            return View(tmodel);

        }

        // POST: Subastas2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "ProductoIDSubastado,CorreoSubastador,FechaPublicado,PrecioMinimo,FechaInicio,FechaFin,Calificacion, FechInicio,FechFin,HoraInicio,HoraFin,sublistas,PublicoEspecial,Mensaje")] SubastaViewModel subasta)
        {
            /*Guardar la fecha de publicacion de la subasta*/
            string fecha = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "MM/dd/yyyy HH:mm:ss";
            DateTime FechaI = DateTime.ParseExact(fecha, format, provider);

            /*Fechas de inicio  de la subasta*/
            TimeSpan tiempo = TimeSpan.Parse(subasta.HoraInicio);
            fecha = subasta.FechInicio + " " + subasta.HoraInicio + ":00";
            DateTime fechaInicioSubasta = DateTime.ParseExact(fecha, format, provider);

            /*Fecha de finalizacion de la subasta*/
            fecha = subasta.FechFin + " " + subasta.HoraFin + ":00";
            DateTime fechaFinSubasta = DateTime.ParseExact(fecha,format, provider);
    

            string correoActual = (string)HttpContext.Session["Correiro"];
            Subasta subastaPrueba = new Subasta();
            if (subasta.sublistas != null) {
                if (ModelState.IsValid)
                {
                    subastaPrueba.ProductoIDSubastado = subasta.ProductoIDSubastado;
                    subastaPrueba.CorreoSubastador = correoActual;
                    subastaPrueba.FechaPublicado = FechaI;
                    subastaPrueba.FechaInicio = fechaInicioSubasta;
                    subastaPrueba.FechaFin = fechaFinSubasta;
                    subastaPrueba.PrecioMinimo = subasta.PrecioMinimo;
                    if (subasta.sublistas[1].Selected)
                    {
                        subastaPrueba.PublicoEspecial = 1;
                    }
                    else {
                        if (!subasta.sublistas[0].Selected && !subasta.sublistas[1].Selected) {
                            subastaPrueba.PublicoEspecial = 2;
                        }
                    }

                    subastaPrueba.Mensaje = subasta.Mensaje;
                    db.Subastas.Add(subastaPrueba);
                    db.SaveChanges();
                }
                ViewBag.Hola = "hola";

                var listas = from s in db.Sublistas
                             select s;
                listas = listas.Where(s => s.CorreoDueno == correoActual);

                if (!subasta.sublistas[0].Selected && !subasta.sublistas[1].Selected)
                {
                    for (int i = 2; i < subasta.sublistas.Count(); ++i)
                    {
                        if (subasta.sublistas[i].Selected)
                        {
                            foreach (var item in listas)
                            {
                                if (item.id == Int32.Parse(subasta.sublistas[i].Text))
                                {
                                    //db.spRelacion_Subasta_Visible_A_Insertar(item.CorreoDueno, item.id, subastaPrueba.ProductoIDSubastado, subastaPrueba.CorreoSubastador, subastaPrueba.FechaPublicado);
                                    subastaPrueba.spRelacion_Subasta_Visible_A_Insertar(item.CorreoDueno, item.id, subastaPrueba.ProductoIDSubastado, subastaPrueba.CorreoSubastador, subastaPrueba.FechaPublicado);
                                }
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            //ViewBag.ProductoIDSubastado = new SelectList(db.Productoes, "ProductoID", "Nombre", subasta.Subastas.ProductoIDSubastado);
            return View();
        }

        public ActionResult misSubastas()
        {
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            var subastas = db.Subastas.Include(s => s.Producto);
            if (correoActual != null) //Esto se supone que saca la información de la cookie del correo 
                return View(subastas.ToList());
            else
                return RedirectToAction("IniciarSesion", "Registrado");
        }

        public ActionResult OfertarEnSubasta(int? idProducto, string correo, DateTime fechaPublicado)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            
            string fecha = fechaPublicado.ToString("MM/dd/yyyy HH:mm:ss");
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "MM/dd/yyyy HH:mm:ss";
            DateTime FechaI = DateTime.ParseExact(fecha, format, provider);
            /**/
            Subasta subast = db.Subastas.Find(idProducto, correo,FechaI);
            if (subast == null)
            {
                return RedirectToAction("RecursoNoEncontrado", "Subastas");
            }
            subast.FechaPublicado = FechaI;
            SubastaViewModel tmodel = new SubastaViewModel();
            
            /**/
            Relacion_ClienteOfertaEnSubasta ofertarSubasta = new Relacion_ClienteOfertaEnSubasta();
            ofertarSubasta.ProductoIDSubastado = (int)idProducto;
            ofertarSubasta.CorreoSubastador = correo;
            ofertarSubasta.FechaPublicado = FechaI;
            tmodel.Subastas = subast;
            tmodel.relacion_ClienteOfertaEnSubastas = ofertarSubasta;
           

            var ofertas = db.Relacion_ClienteOfertaEnSubasta.Include(c => c.Subasta);
            var oferta = from p in db.Relacion_ClienteOfertaEnSubasta
                         select p;
            oferta = oferta.Where(p => p.ProductoIDSubastado == idProducto);

            //double? maximo = oferta.Max(o => o.PrecioOfrecido);
            if (oferta.Any())
            {
                ViewBag.OfertaMaxima = oferta.Max(o => o.PrecioOfrecido);
            }
            else
            {
                ViewBag.OfertaMaxima = 0;
            }
            //float precioMaximo = 0;
            // Relacion_ClienteOfertaEnSubasta ofertaMayor = db.Relacion_ClienteOfertaEnSubastas.Max(s => s.PrecioOfrecido && s.ProductoIDSubastado == idProducto);

            // string correoActual = (string)HttpContext.Session["Correiro"];
            //var insertar = db.Productoes.SqlQuery("[dbo].[Producto_InsertarProducto]").ToList();
            // if (correoActual != null) //Esto se supone que saca la información de la cookie del correo 
            return View(tmodel);
            // else
            //      return RedirectToAction("IniciarSesion", "Registrado");
            //db.Relacion_ClienteOfertaEnSubastas =
        }

        [HttpPost]
    
        public ActionResult OfertarEnSubasta([Bind(Include = "CorreoOfertador, ProductoIDSubastado, CorreoSubastador, FechaPublicado, PrecioOfrecido")] SubastaViewModel ofertaSubasta)
        {
            DateTime var = ofertaSubasta.FechaPublicado;
            string fecha = ofertaSubasta.FechaPublicado.ToString("MM/dd/yyyy HH:mm:ss");
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "MM/dd/yyyy HH:mm:ss";
            DateTime FechaI = DateTime.ParseExact(fecha, format, provider);
            //FechaI = FechaI.AddHours(12);
            ofertaSubasta.FechaPublicado = FechaI;

            string correoActual = (string)HttpContext.Session["Correiro"];
            //ofertaSubasta.CorreoOfertador = correoActual;
            var oferta = from p in db.Relacion_ClienteOfertaEnSubasta
                         select p;
            oferta = oferta.Where(p => p.ProductoIDSubastado == ofertaSubasta.ProductoIDSubastado);
            Relacion_ClienteOfertaEnSubasta ofertar = new Relacion_ClienteOfertaEnSubasta();
            /*ofertarSubastaParam.ProductoIDSubastado = ofertaSubasta.ProductoIDSubastado;
            ofertarSubastaParam.CorreoSubastador = ofertaSubasta.CorreoSubastador;
            ofertarSubastaParam.FechaPublicado = ofertaSubasta.FechaPublicado;*/
            Producto prod = db.Productoes.Find(ofertaSubasta.ProductoIDSubastado, ofertaSubasta.CorreoSubastador);
            Subasta sub = db.Subastas.Find(ofertaSubasta.ProductoIDSubastado, ofertaSubasta.CorreoSubastador, ofertaSubasta.FechaPublicado);
            ofertar.ProductoIDSubastado = ofertaSubasta.ProductoIDSubastado;
            ofertar.CorreoOfertador = correoActual;
            ofertar.CorreoSubastador = ofertaSubasta.CorreoSubastador;
            ofertar.FechaPublicado = FechaI;
            ofertar.PrecioOfrecido = ofertaSubasta.PrecioOfrecido;
            /* ViewBag.Nombre = prod.Nombre;
            ViewBag.Condicion = prod.Condicion;*/
            /*if (sub.PrecioMinimo != null)
            {
                ViewBag.PrecioMinimo = sub.PrecioMinimo;
            }*/

            // ViewBag.Descripcion = prod.Descripcion;
            // ViewBag.FechaInicio = sub.FechaInicio;
            //ViewBag.FechaFin = sub.FechaFin;
             var body1 = $"Truques@UCR informa: \n El usuario {ofertar.CorreoOfertador} ha ofertado: {ofertar.PrecioOfrecido} en la subasta por su producto: {prod.Nombre}";
             body1.Replace("\n", Environment.NewLine);
             



            //Revisa si la persona ingreso un precio menor al mayor ya ofrecido
            /*if (oferta.Any())
            {
                if (ofertaSubasta.PrecioOfrecido < oferta.Max(o => o.PrecioOfrecido))
                {
                    ViewBag.OfertaMaxima = oferta.Max(o => o.PrecioOfrecido);
                    ViewBag.Aviso = "No puede ofrecer un precio menor al precio mayor ya ofrecido";
                    return View(ofertarSubastaParam);
                }
            }*/
            //Para ver si ya esta persona oferto algo en esa misma subasta
            if (db.Relacion_ClienteOfertaEnSubasta.Find(correoActual, ofertaSubasta.ProductoIDSubastado, ofertaSubasta.CorreoSubastador, FechaI) != null)
            {
                editarOferta(ofertar);
                return RedirectToAction("Index");
            }
            else
            if (ModelState.IsValid)//error The value '06/24/2020 06:01:56,24/06/2020 06:01:56' is not valid for FechaPublicado
            {
                
                db.Relacion_ClienteOfertaEnSubasta.Add(ofertar);
                db.SaveChanges();
                EnviarCorreo(ofertaSubasta.CorreoSubastador, body1);
                //EnviarCorreo(ofertaSubasta.CorreoSubastador, body2);
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }

            //ViewBag.ProductoIDSubastado = new SelectList(db.Relacion_ClienteOfertaEnSubastas, "ProductoID", "Nombre", ofertaSubasta.ProductoIDSubastado);
            //return View(ofertaSubasta);
        }

        [HttpPost]
        
        public ActionResult editarOferta([Bind(Include = "CorreoOfertador, ProductoIDSubastado, CorreoSubastador, FechaPublicado, PrecioOfrecido")] Relacion_ClienteOfertaEnSubasta ofertaSubasta)
        {
            Producto prod = db.Productoes.Find(ofertaSubasta.ProductoIDSubastado, ofertaSubasta.CorreoSubastador);
            var body1 = $"Truques@UCR informa: \n El usuario {ofertaSubasta.CorreoOfertador} ha ofertado: {ofertaSubasta.PrecioOfrecido} en la subasta por su producto: {prod.Nombre}";
            body1.Replace("\n", Environment.NewLine);
           /* var body2 = $"Truques@UCR informa: \n El usuario {ofertaSubasta.CorreoOfertador} ha enviado una oferta por una subasta";
            body2.Replace("\n", Environment.NewLine);*/
            if (ModelState.IsValid)
            {

                db.Set<Relacion_ClienteOfertaEnSubasta>().AddOrUpdate(ofertaSubasta);
                EnviarCorreo(ofertaSubasta.CorreoSubastador, body1);
                //EnviarCorreo(ofertaSubasta.CorreoSubastador, body2);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: Subastas2/Edit/5
        public ActionResult Edit(int? idProducto, string correo, DateTime? fecha)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            Subasta subasta = db.Subastas.Find(idProducto, correo,fecha);
            Producto pro = db.Productoes.Find(idProducto, correo);
            if (pro == null)
            {
                return RedirectToAction("RecursoNoEncontrado", "Subastas");
            }
            if (subasta == null)
            {
                return RedirectToAction("RecursoNoEncontrado", "Subastas");
            }
            SubastaViewModel tmodel = new SubastaViewModel();
            tmodel.Productos = pro;
            tmodel.Subastas = subasta;
            return View(tmodel);
        }

        // POST: Subastas2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit([Bind(Include = "ProductoIDSubastado,CorreoSubastador,FechaPublicado,PrecioMinimo,FechaInicio,FechaFin,Calificacion, FechInicio,FechFin,HoraInicio,HoraFin")] SubastaViewModel subasta)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "MM/dd/yyyy HH:mm:ss";
            Subasta subast = new Subasta();
            /*Fechas de inicio  de la subasta*/
            TimeSpan tiempo = TimeSpan.Parse(subasta.HoraInicio);
            string fecha = subasta.FechInicio + " " + subasta.HoraInicio + ":00";
            DateTime fechaInicioSubasta = DateTime.ParseExact(fecha, format, provider);

            /*Fecha de finalizacion de la subasta*/
            fecha = subasta.FechFin + " " + subasta.HoraFin + ":00";
            DateTime fechaFinSubasta = DateTime.ParseExact(fecha, format, provider);
            if (ModelState.IsValid)
            {
                subast.ProductoIDSubastado = subasta.ProductoIDSubastado;
                subast.CorreoSubastador = correoActual;
                subast.PrecioMinimo = subasta.PrecioMinimo;
                subast.FechaPublicado = subasta.FechaPublicado;
                subast.FechaInicio = fechaInicioSubasta;
                subast.FechaFin = fechaFinSubasta;
                db.Entry(subast).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ProductoIDSubastado = new SelectList(db.Productoes, "ProductoID", "Nombre", subasta.ProductoIDSubastado);
            return View(subasta);
        }

        // GET: Subastas2/Delete/5
        public ActionResult Delete(int? idProducto, string correo, DateTime? fecha)
        {
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            if (idProducto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subasta subasta = db.Subastas.Find(idProducto, correo, fecha);
            if (subasta == null)
            {
                return RedirectToAction("RecursoNoEncontrado", "Subastas");
            }
            return View(subasta);
        }

        // POST: Subastas2/Delete/5
        [HttpPost, ActionName("Delete")]
 
        public ActionResult DeleteConfirmed(int? idProducto, string correo, DateTime fecha)
        {
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            Subasta subasta = db.Subastas.Find(idProducto, correo, fecha);
            subasta.Estado = false;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListaProductos(string searchBy, string currentFilter, string searchString, int? page)
        {
            //var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            ViewBag.CurrentSort = searchBy;
            ViewBag.NameSortParm = String.IsNullOrEmpty(searchBy) ? "Name desc" : "";
            ViewBag.SortPriceParm = searchBy == "Price" ? "Price desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
           

            /*Estas consultas son para evitar que la persona pueda subastar el producto cuando ya ha sido subastado*/
            var productos = from pr in db.Productoes
                            select pr;
            productos = productos.Where(pr => pr.CorreoCliente == correoActual);
            productos = from l in productos
                        where (db.Subastas.Any(item => item.ProductoIDSubastado == l.ProductoID && DateTime.Compare(item.FechaFin, DateTime.Now) < 0)
                                || !(db.Subastas.Any(item => item.ProductoIDSubastado == l.ProductoID)))
                        select l;

            var subastas = from s in db.Subastas
                           select s;

           
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
                case "Name desc":
                    productos = productos.OrderByDescending(p => p.Nombre);
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
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(productos.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult EnviarCorreo(string correo, string bodyMessage)
        {
            var fromAddress = new MailAddress("imapdemoecci2020@gmail.com", "Trueques UCR");
            var toAddress = new MailAddress(correo, "Estimado Usuario");
            const string fromPassword = "Ab654321!";
            const string subject = "Ha recibido una nueva oferta por la subasta";
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

        public ActionResult RecursoNoEncontrado()
        {
            return View();
        }
    }
}

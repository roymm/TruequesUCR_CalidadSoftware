using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Antlr.Runtime;
using ICSharpCode.AvalonEdit.Utils;
using System.Dynamic;
using Proyecto_Inge_Bases_Web.Models;
using Newtonsoft.Json;
using System.Data.Entity.Core.Mapping;
using PagedList;
using System.Text.RegularExpressions;
using System.Data.Entity.Core.Objects;

namespace Proyecto_Inge_Bases_Web.Controllers
{

    public class ClienteController : Controller
    {
        private TempPIEntities db = new TempPIEntities();

        // GET: Cliente
        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.Registrado);
            return View(clientes.ToList());
        }



        //Método para usar en imagen
        public byte[] ConvertToByte(HttpPostedFileBase file2)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file2.InputStream);
            imageByte = rdr.ReadBytes((int)file2.ContentLength);
            return imageByte;
        }

        // Guarda datos del user
        [HttpPost]
        public ActionResult EdicionPerfil(FormCollection form)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "MM/dd/yyyy";
            string nombre = form["Nombre"].ToString();
            string apellido1 = form["Apellido1"].ToString();
            string apellido2 = form["Apellido2"].ToString();
            string edad = form["Edad"].ToString();
            string direccion = form["Direccion"].ToString();
            DateTime edad2 = DateTime.ParseExact(edad, format, provider);
            // Actividad supervisada , ID de historia = OC- 1.6 , Daniel Sancho B66676, Joshua Ramirez, Se lee la categoría seleccionada(Aún solo lee una) y se procede a guardar en la base 
            // de datos(Hay que hacer algunas modificaciones en la base de datos por lo tanto no se logra guardar aún, solo captarla.)
            // var categoria = form["Categoria"]; // Se lee la categoria desde la vista
            Registrado usuario = db.Registradoes.Find(HttpContext.Session["Correiro"]);
            Cliente cliente1 = db.Clientes.Find(HttpContext.Session["Correiro"]);
            //Actividad Supervisada Pair programming, HISTORIA= OC-1.6, DANIEL SANCHO Y JOSHUA RAMIREZ, se obtiene la info del canton de la vista y se guarda en la base de datos
            //se define la variable canton para guardar los datos obtenidos del form
            var canton = form["Canton"];
            // se hace una consulta a la base de datos para comparar el nombre guardado la variable con el nombre del canton en la base
            Canton canton1 = db.Cantons.SingleOrDefault(cantonx => cantonx.Nombre == canton);
            // se guarda el canton id en la base de datos del cliente actual
            cliente1.Canton = canton1.id;

            HttpPostedFileBase file = Request.Files["file2"];//Para leer imagen de vista
            byte[] imagen7 = ConvertToByte(file);
            if (imagen7.Length == 0)//if para que no se guarde una imagen vacia despues de que el usuario ya ingresó una imagen o no ha ingresado y decide ingresar de nuevo a la página de editar perfil
            { }
            else
            { cliente1.FotoPerfil = imagen7; }

            string checkResp = form["checkbox1"]; //lee el valor del checkbox
            bool checkRespB = Convert.ToBoolean(checkResp);
            cliente1.BloquearNotificaciones = checkRespB;// Guarda en base true si el user checkea la opción de no resivir notif

            //Aqui empieza el código donde se guardan las categorías que elija el user.
            string[] arrayCategorias = { "Entretenimiento", "Educación", "Salud y Belleza", "Deportes", "Ferreteria", "Hogar", "Peliculas", "Juegos", "Libros", "Tecnología", "Material educativo", "Articulos educativos", "Acción", "Ropa", "Calzado", "Camisas", "Cocina", "Vehículo", "Carreta", "Instrumentos Musicales" };
            /*for (int i = 0; i < arrayCategorias.Length; i++)
            {
                string categoriaMarcada = form[arrayCategorias[i]];    //lee el valor del checkbox
                string nombreCategoria = arrayCategorias[i]; // String para leer el id de la categoria en tabla Categorias
                bool categoriaMarcadax = Convert.ToBoolean(categoriaMarcada);
                Categoria categoria = db.Categorias.SingleOrDefault(categoriax => categoriax.Nombre == nombreCategoria);// Se lee la categoria con el nombre previo
                Relacion_ClienteTieneInteresCategoria relacionTemporal = db.Relacion_ClienteTieneInteresCategoria.SingleOrDefault(categoriax => categoriax.CorreoCliente == usuario.Correo && categoriax.IDCategoria == categoria.CategoriaID);// Se localiza la tupla para ver si existe o no en la tabla de relacion, esto para ver si hay que agregarla o ya está agregada
                if (categoriaMarcadax == true && relacionTemporal == null)// Si el checkbox está marcado y si la tupla no existe
                {
                    Relacion_ClienteTieneInteresCategoria r = new Relacion_ClienteTieneInteresCategoria();
                    r.CorreoCliente = HttpContext.Session["Correiro"].ToString();// Asigna el correo para agregar en tupla 
                    r.IDCategoria = categoria.CategoriaID;// Asigna id de categoria para agregar tupla 
                    db.Relacion_ClienteTieneInteresCategoria.Add(r);// Agrega tupla con valores previos.

                }
                else if (relacionTemporal != null && categoriaMarcadax == false)// Si el usuario no marca el checkbox y la tupla si existe,  el siguiente código la elimina
                {
                    db.Relacion_ClienteTieneInteresCategoria.Remove(db.Relacion_ClienteTieneInteresCategoria.SingleOrDefault(categoriax => categoriax.CorreoCliente == usuario.Correo && categoriax.IDCategoria == categoria.CategoriaID));
                }
            }*/
            //Aqui termina el código categorias
            cliente1.FechaNacimiento = edad2;
            cliente1.DireccionExacta = direccion;
            usuario.Nombre = nombre;
            usuario.Apellido1 = apellido1;
            usuario.Apellido2 = apellido2;
            db.SaveChanges();
            @Session["Username"] = nombre;// Ver para que servia no recuerdo
            return RedirectToAction("PerfilCompleto", "Registrado");
        }

        public ActionResult EditarPerfil(string ChangeCantonId)
        {
            Cliente cliente1 = db.Clientes.Find(HttpContext.Session["Correiro"]);
            Registrado registrado2 = db.Registradoes.Find(HttpContext.Session["Correiro"]);

            if (cliente1.Canton != null)
            {
                var cantonid = cliente1.Canton;
                Canton cantonx = db.Cantons.Find(cantonid);
                var cantoname = cantonx.Nombre;
                var cantonprovincia = cantonx.NombreProvincia;

                var pais = db.Pais.Where(p => p.Nombre != null)
                  .Select(i => i.Nombre);
                ViewBag.Pais = new SelectList(pais);

                var provincia = db.Provincias.Where(p => p.Nombre == cantonprovincia).Concat(db.Provincias.Where(p => p.Nombre != cantonprovincia))// Filtrar provincia del canton seleccionado y mostrar las demás sin repetir
                    .Select(i => i.Nombre);
                ViewBag.Provincia = new SelectList(provincia);

                var canton = db.Cantons.Where(p => p.Nombre == cantoname).Concat(db.Cantons.Where(p => (p.NombreProvincia == cantonprovincia && p.Nombre != cantoname)))// FIltrar canton seleccionado y mostrar las demás sin repetir.
                    .Select(i => i.Nombre);
                ViewBag.Canton = new SelectList(canton);

            }
            else
            { // Muestra ubicaciones para usuario recien registrado ...
                var pais = db.Pais.Where(p => p.Nombre != null)
                   .Select(i => i.Nombre);
                ViewBag.Pais = new SelectList(pais);

                var provincia = db.Provincias.Where(p => p.Nombre != null)
                    .Select(i => i.Nombre);
                ViewBag.Provincia = new SelectList(provincia);

                var canton = db.Cantons.Where(p => p.NombreProvincia == "Alajuela")
                    .Select(i => i.Nombre);
                ViewBag.Canton = new SelectList(canton);

            }

            var categoria = db.Categorias.Where(p => p.CategoriaID != 0)
              .Select(i => i.Nombre)
              .Distinct();
            ViewBag.Categorias = new SelectList(categoria);



            // Aqui se muestran los datos del usuario en págnia de perifl
            //Este if es para poder mostrar la imagen del perfil de usuario, si el user no ha cargado imagen que se cargue la que está por default.
            if (cliente1.FotoPerfil == null)
            {
                byte[] image = new WebClient().DownloadData("https://ucrindex.ucr.ac.cr/wp-content/plugins/all-in-one-seo-pack/images/default-user-image.png");
                var base64Image = Convert.ToBase64String(image);
                TempData["img"] = base64Image;
            }
            else
            {
                var base64Image = Convert.ToBase64String(cliente1.FotoPerfil);
                TempData["img"] = base64Image;
            }

            //  Aquí empieza código para mostrar categorias ya marcadas por el user 
            string[] arrayCategorias = { "Entretenimiento", "Educación", "Salud y Belleza", "Deportes", "Ferreteria", "Hogar", "Peliculas", "Juegos", "Libros", "Tecnología", "Material educativo", "Articulos educativos", "Acción", "Ropa", "Calzado", "Camisas", "Cocina", "Vehículo", "Carreta", "Instrumentos Musicales" };
            string[] arrayTempdatas = new string[20];// Array de tempdatas que se mandan a la vista
            string nombrecategoriatemporal;// Para buscar la categoria del momento en la tabla categorias
            Categoria categoria1;
            Relacion_ClienteTieneInteresCategoria rtemp;// relacion para buscar categorias del user en tabla
            for (int i = 0; i < arrayCategorias.Length; i++) {
                nombrecategoriatemporal = arrayCategorias[i];// para buscar la categoria
                categoria1 = db.Categorias.SingleOrDefault(categoriax => categoriax.Nombre == nombrecategoriatemporal);// Devuelve objeto categoria que lleva de nombre la categoria del momento.
                rtemp = null;
                //rtemp = db.Relacion_ClienteTieneInteresCategoria.SingleOrDefault(categoriax => categoriax.CorreoCliente == registrado2.Correo && categoriax.IDCategoria == categoria1.CategoriaID);// temporal para ver si el user ya tiene la categoria guardada.
                if (rtemp != null)// si ya tiene la categoria entonces el tempdata se pone en true para que sea automarcado en la vista
                {
                    arrayTempdatas[i] = "True";
                }
                else // si no tiene la categoria entonces el tempdata se pone en false para que no sea automarcado en la vista.
                {
                    arrayTempdatas[i] = "False";
                }
            }// Tempdatas para poder autoseleccionar en la vista las categorias ya  guardas.
            TempData["EntretenimientoMarcado"] = arrayTempdatas[0];
            TempData["EducaciónMarcado"] = arrayTempdatas[1];
            TempData["SaludMarcado"] = arrayTempdatas[2];
            TempData["DeportesMarcado"] = arrayTempdatas[3];
            TempData["FerreteriaMarcado"] = arrayTempdatas[4];
            TempData["HogarMarcado"] = arrayTempdatas[5];
            TempData["PeliculasMarcado"] = arrayTempdatas[6];
            TempData["JuegosMarcado"] = arrayTempdatas[7];
            TempData["LibrosMarcado"] = arrayTempdatas[8];
            TempData["TecnologiaMarcado"] = arrayTempdatas[9];
            TempData["MaterialEdMarcado"] = arrayTempdatas[10];
            TempData["ArticulosEdMarcado"] = arrayTempdatas[11];
            TempData["AccionMarcado"] = arrayTempdatas[12];
            TempData["RopaMarcado"] = arrayTempdatas[13];
            TempData["CalzadoMarcado"] = arrayTempdatas[14];
            TempData["CamisasMarcado"] = arrayTempdatas[15];
            TempData["CocinaMarcado"] = arrayTempdatas[16];
            TempData["VehículoMarcado"] = arrayTempdatas[17];
            TempData["CarretaMarcado"] = arrayTempdatas[18];
            TempData["InstrumentosMarcado"] = arrayTempdatas[19];

            //Aqui termina el código par mostrar las categorias ya marcadas por el user


            // Para checkear box en vista si ya fue checkeadoa nteriormente
            if (cliente1.BloquearNotificaciones == true)
            {
                TempData["checked"] = "checked";
            }
            else
            {
                TempData["checked"] = "";

            }


            TempData["NombreUsuario"] = registrado2.Nombre;
            TempData["Apellido1"] = registrado2.Apellido1;
            TempData["Apellido2"] = registrado2.Apellido2;
            TempData["Direccion"] = cliente1.DireccionExacta;
            TempData["Notificaciones"] = cliente1.BloquearNotificaciones; // ....................
            string edadx = cliente1.FechaNacimiento.ToString();
            if (edadx != "")
            {
                string edadtemp1 = "";
                string edadtemp2 = "";
                string edadtemp3 = "";
                string edadfinal = "";
                int x = edadx.Length;
                if (edadx.Length == 18)
                {
                    if (edadx.Substring(1, 1) == "/")
                    {
                        edadx = "0" + edadx;
                    }
                    else
                    {
                        edadtemp1 = edadx.Substring(0, 2);
                        edadtemp2 = edadx.Substring(3, 1);
                        edadtemp2 = "0" + edadtemp2;
                        edadtemp3 = edadx.Substring(4, edadx.Length - 4);
                        edadx = edadtemp1 + "/" + edadtemp2 + edadtemp3;
                    }
                }
                else if (edadx.Length == 17)
                {
                    edadx = "0" + edadx;
                    edadtemp1 = edadx.Substring(0, 2);
                    edadtemp2 = edadx.Substring(3, 1);
                    edadtemp2 = "0" + edadtemp2;
                    edadtemp3 = edadx.Substring(4, edadx.Length - 4);
                    edadx = edadtemp1 + "/" + edadtemp2 + edadtemp3;
                }
                edadtemp1 = edadx.Substring(0, 2);
                edadtemp2 = edadx.Substring(3, 2);
                edadtemp3 = edadx.Substring(5, edadx.Length - 5);
                edadfinal = edadtemp2 + "/" + edadtemp1 + edadtemp3;

                TempData["Edad"] = edadfinal;
                return View();
            }
            else
            {
                TempData["Edad"] = "01/01/2000";
                return View();
            }
        }

        public ActionResult GetProvincia(string nombrePais)
        {
            var provincia = db.Provincias.Where(p => p.NombrePais == nombrePais.Trim())
            .Select(i => i.Nombre);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (string pr in provincia)
            {
                items.Add(new SelectListItem() { Text = pr, Value = pr });
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCanton(string nombreProvincia)
        {
            var canton = db.Cantons.Where(p => p.NombreProvincia == nombreProvincia.Trim())
            .Select(i => i.Nombre);
            List<SelectListItem> items = new List<SelectListItem>();
            Console.WriteLine(canton);
            Console.WriteLine(nombreProvincia);

            foreach (string c in canton)
            {
                Console.WriteLine(c);
                items.Add(new SelectListItem() { Text = c, Value = c });
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // Método que se ejecuta cuando se registra el usuario, activa la cuenta y retorna vista.
        public ActionResult EditarPerfilRegistrandose()
        {
            // RegistradoController cliente = new RegistradoController();
            Registrado usuario = db.Registradoes.Find(HttpContext.Session["Username2"]);
            usuario.EstadoCuenta = true;
            db.SaveChanges();
            //cliente.Login(usuario);
            return RedirectToAction("IniciarSesion", "Registrado");
        }
        public ActionResult PerfilCliente(int? page)
        {
            var correoU = HttpContext.Session["Correiro"];
            string correoUser = correoU.ToString();

            List<VistaMisProducto> misProductos = new List<VistaMisProducto>();
            misProductos = db.VistaMisProductos.Where(c => c.CorreoCliente == correoUser).ToList();

            var productos = from a in misProductos
                            from p in db.Productos_Info_Clientes
                            where p.ProductoID == a.ProductoID
                            select p;
            productos = productos.OrderBy(p => p.Nombre);


            //Mostrar 9 productos por página
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            // Para cambiar el layout en el caso de que sea el administrador el que este viendo la pagina
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);
            var view = View(productos.ToPagedList(pageNumber, pageSize));

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




        public ActionResult ListaAmigos()
        {

            var correoU = HttpContext.Session["Correiro"];
            string correoUser = correoU.ToString();
            List<VistaAmigo> lista_A = db.VistaAmigos.Where(c => c.CorreoDueno == correoUser).ToList();
            List<VistaNoAmigo> lista_B = db.VistaNoAmigoes.Where(c => c.dueno == correoUser).Distinct().ToList();
            ViewBag.Amigos = lista_A;
            ViewBag.NoAmigos = lista_B;
            MostrarListas listas = new MostrarListas();
            listas.Amigos = lista_A;
            listas.NoAmigos = lista_B;


            return View(listas);
        }



        public ActionResult EliminarAmigos(string correo) {

            var correoU = HttpContext.Session["Correiro"];
            string correoUser = correoU.ToString();

            ListaAmigo amigo = db.ListaAmigos.Find(correoUser, correo);
            db.ListaAmigos.Remove(amigo);
            db.SaveChanges();
            return RedirectToAction("ListaAmigos");

        }

        public ActionResult AgregarAmigos(string correo)
        {
            var correoU = HttpContext.Session["Correiro"];
            string correoUser = correoU.ToString();

            ListaAmigo amigo = new ListaAmigo();
            amigo.CorreoDueno = correoUser;
            amigo.CorreoAmigo = correo;
            db.ListaAmigos.Add(amigo);
            db.SaveChanges();
            return RedirectToAction("ListaAmigos");

        }



        public ActionResult ListaDeAmigosSubListas(int? ide)
        {

            var correoU = HttpContext.Session["Correiro"];
            string correoUser = correoU.ToString();
            List<PanelSublista> lista_S = db.PanelSublistas.Where(c => c.CorreoDueno == correoUser).ToList();
            MostrarListas listas = new MostrarListas();
            listas.Sublistas = lista_S;
            if (listas.EstaEnSublistas == null)
            {
                List<VistaEstaEnSublista> lista_E = new List<VistaEstaEnSublista>();
                listas.EstaEnSublistas = lista_E;
                List<VistaEstaEnSublista> lista_N = new List<VistaEstaEnSublista>();
                listas.NoEsta = lista_N;
            }
            if (ide != null)
            {
                List<VistaEstaEnSublista> lista_N = new List<VistaEstaEnSublista>();
                List<VistaEstaEnSublista> lista_E = db.VistaEstaEnSublistas.Where(c => c.SublistaID == ide).ToList();
                List<VistaEstaEnSublista> lista_Todos = db.VistaEstaEnSublistas.ToList();
                List<VistaAmigo> lista_A = db.VistaAmigos.Where(c => c.CorreoDueno == correoUser).ToList();
                foreach (var item in lista_A) {

                    ObjectParameter returnId = new ObjectParameter("existe", typeof(int)); //Create Object parameter to receive a output value.It will behave like output parameter  
                    int esta = db.ExisteEnSublista(ide, item.CorreoAmigo, returnId);
                    if (returnId.Value.Equals(0)) {
                        VistaEstaEnSublista user = new VistaEstaEnSublista();
                        user.CorreoDueno = item.CorreoDueno;
                        user.CorreoAmigo = item.CorreoAmigo;
                        user.Nombre = item.Nombre;
                        user.Apellido1 = item.Apellido1;
                        user.Apellido2 = item.Apellido2;
                        user.SublistaID = (int)ide;
                        lista_N.Add(user);
                    }
                }
                listas.NoEsta = lista_N;
                listas.EstaEnSublistas = lista_E;

            }
            return View(listas);
        }

        public ActionResult AgregarASublista(string correo, int idSub)
        {
            var correoU = HttpContext.Session["Correiro"];
            string correoUser = correoU.ToString();

            Relacion_Sublista_Amigos amigo = new Relacion_Sublista_Amigos();
            amigo.CorreoDueno = correoUser;
            amigo.CorreoAmigo = correo;
            amigo.CorreoDuenoSublista = correoUser;
            amigo.SublistaID = idSub;
            db.Relacion_Sublista_Amigos.Add(amigo);
            db.SaveChanges();
            return RedirectToAction("ListaDeAmigosSubListas");

        }

        public ActionResult EliminarDeSublista(string correo, int idSub)
        {
            var correoU = HttpContext.Session["Correiro"];
            string correoUser = correoU.ToString();
            Relacion_Sublista_Amigos amigo = db.Relacion_Sublista_Amigos.Find(correoUser, correo, correoUser, idSub);
            db.Relacion_Sublista_Amigos.Remove(amigo);
            db.SaveChanges();
            return RedirectToAction("ListaDeAmigosSubListas");
        }
        [HttpPost]
        public ActionResult CrearSublista(FormCollection form)
        {
            var correoU = HttpContext.Session["Correiro"];
            string correoUser = correoU.ToString();
            string nombreLista = form["agregarSublista"].ToString();
            Sublista nuevo = new Sublista();
            nuevo.CorreoDueno = correoUser;
            nuevo.NombreSublista = nombreLista;
            db.Sublistas.Add(nuevo);
            db.SaveChanges();
            return RedirectToAction("ListaDeAmigosSubListas");
        }

            // GET: Cliente/Details/5
            public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            ViewBag.Correo = new SelectList(db.Registradoes, "Correo", "Contrasena");
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Correo,FechaNacimiento,Pais,Provincia,Canton,DireccionExacta,FotoPerfil,EstadoCuenta,FechaCierre,CalificacionPromedio,ClienteID")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Correo = new SelectList(db.Registradoes, "Correo", "Contrasena", cliente.Correo);
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Correo = new SelectList(db.Registradoes, "Correo", "Contrasena", cliente.Correo);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Correo,FechaNacimiento,Pais,Provincia,Canton,DireccionExacta,FotoPerfil,EstadoCuenta,FechaCierre,CalificacionPromedio,ClienteID")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Correo = new SelectList(db.Registradoes, "Correo", "Contrasena", cliente.Correo);
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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
    }
}

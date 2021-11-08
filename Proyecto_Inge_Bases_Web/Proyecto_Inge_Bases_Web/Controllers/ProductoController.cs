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
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Core.Objects;
using Proyecto_Inge_Bases_Web.ViewModels;

namespace Proyecto_Inge_Bases_Web.Controllers
{
    public class ProductoController : Controller
    {
        private TempPIEntities db = new TempPIEntities();


        // GET: Producto
        /**
            @Param: sortOrder. string que indica de que manera se va a ordenar la lista.
            @Param: currentFilter. Permite saber la configuracion de filtrado
            @Param: searchString. Recibe el string con lo que desea buscar.
            @Param: page. Permite saber cual es la pagina a mostrar
        */

        public ActionResult Index(string searchBy, string currentFilter, string searchString, int? page)
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


            //Producto producto;
            // Es para que me salgan solo mis productos, (yo siendo el usuario logueado)

            productos = productos.Where(p => p.Estado == true);

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

        // GET: Producto/Details/5
        public ActionResult Details(int? id, string correo)
        {

            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            string correoActual = (string)HttpContext.Session["Correiro"];



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
            ProductoCreateViewModel productoCreateViewModel = new ProductoCreateViewModel()
            {
                // Ojo, no voy a cambiar la referencia de producto, pero para respetar
                // el view model deberia ser productoCreatViewModel.producto
                Producto = producto,
                Categoria = new List<Categoria>(db.Categorias),
                selected = new List<bool>()
            };

            var productosVirtuales = from p in db.Virtuals
                                     select p;
            ViewBag.productosVirtuales = productosVirtuales;


            return View(producto);
        }

        // GET: Producto/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categoria = new SelectList(db.Categorias, "CategoriaId", "Nombre");
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (correoActual != null) //Esto se supone que saca la información de la cookie del correo 
                return View();
            else
                return RedirectToAction("IniciarSesion", "Registrado"); // Me devuelve a registrarme
        }

        // POST: Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "ProductoID,CorreoCliente,Nombre,PrecioEstimado,Condicion,Descripcion,Publicado,ProductoImagen1,ProductoImagen2,ProductoImagen3,FechaRegistrado")] Producto producto, HttpPostedFileBase Archivo_Imagen1, HttpPostedFileBase Archivo_Imagen2, HttpPostedFileBase Archivo_Imagen3, FormCollection form)
        {

            ProductoCreateViewModel productoCreateViewModel = new ProductoCreateViewModel()
            {
                // Ojo, no voy a cambiar la referencia de producto, pero para respetar
                // el view model deberia ser productoCreatViewModel.producto
                Producto = producto,
                Categoria = new List<Categoria>(db.Categorias),
                selected = new List<bool>()
            };
            ViewBag.Categoria = new SelectList(db.Categorias, "CategoriaId", "Nombre");

            producto.FechaRegistrado = DateTime.Now;

            string derechosAutor = Convert.ToString(form["Derechos"].ToString());
            string tipoArchivo = Convert.ToString(form["TipoArchivo"].ToString());
            string fuentes = Convert.ToString(form["Fuentes"].ToString());

            string correoActual = (string)HttpContext.Session["Correiro"];

            if (correoActual != null) //Esto se supone que saca la información de la cookie del correo 
                producto.CorreoCliente = correoActual;
            else
                return RedirectToAction("IniciarSesion", "Registrado"); // Me devuelve a registrarme

            byte[] imagenP1 = { };
            byte[] imagenP2 = { };
            byte[] imagenP3 = { };

            if (Archivo_Imagen1 != null)
            {
                imagenP1 = ConvertToByte(Archivo_Imagen1);
                if (imagenP1.Length != 0) { producto.ProductoImagen1 = imagenP1; }
            }

            if (Archivo_Imagen2 != null)
            {
                imagenP1 = ConvertToByte(Archivo_Imagen2);
                if (imagenP2.Length != 0) { producto.ProductoImagen2 = imagenP2; }
            }

            if (Archivo_Imagen3 != null)
            {
                imagenP1 = ConvertToByte(Archivo_Imagen3);
                if (imagenP3.Length != 0) { producto.ProductoImagen3 = imagenP3; }
            }

            //Debug.WriteLine("img1: " + producto.ProductoImagen1);
            //Debug.WriteLine("img2: " + producto.ProductoImagen2);
            //Debug.WriteLine("img3: " + producto.ProductoImagen3);


            if (ModelState.IsValid)
            {
                //if (producto.InsertProducto(producto) == "ERROR")
                //{
                //    return RedirectToAction("Index", "Producto");
                //}
            }

            string[] resultadosCheckboxes = form["checkboxCategoria"].Split(',');
            List<string> resultadosCheckboxesLista = resultadosCheckboxes.ToList();

            for (int i = 0; i < resultadosCheckboxesLista.Count; i++)
            {
                string resultado = resultadosCheckboxesLista[i];
                if (resultado == "true")
                {
                    resultadosCheckboxesLista.RemoveAt(i + 1);
                }
            }

            List<Categoria> listaCategorias = new List<Categoria>(db.Categorias);

            for (int i = 0; i < resultadosCheckboxesLista.Count; i++)
            {
                if (resultadosCheckboxesLista[i] == "true")
                {
                    listaCategorias[i].Productoes.Add(producto);
                    producto.Categorias.Add(listaCategorias[i]);
                }

            }

           



            int opcion = Convert.ToUInt16(form["TipoProducto"].ToString()); //UInt ya que el selector no tiene valores negativos. Con esta opción guardamos a la tabla físico o virtual.
            //1: Físico.    2: Virtual
            if (opcion == 1)
            {
                insertProductoFisico(producto);
            }
            else
            {
                insertProductoVirtual(producto, derechosAutor, tipoArchivo, fuentes);
            }

            


            return RedirectToAction("Index");

        }


        public void insertProductoFisico(Producto producto)
        {

            string listaCategoriasString = "";
            foreach (Categoria categoria in producto.Categorias)
            {
                listaCategoriasString += categoria.CategoriaID;

                if (categoria != producto.Categorias.Last())
                {
                    listaCategoriasString += ",";
                }
            }

            db.spFisico_Insert(producto.Nombre, producto.CorreoCliente, producto.PrecioEstimado, producto.Condicion, producto.Descripcion, 
                producto.ProductoImagen1, producto.ProductoImagen2, producto.ProductoImagen3, producto.Publicado, DateTime.Now, listaCategoriasString
           );
        }

        public void insertProductoVirtual(Producto producto, string derechosAutor, string tipoArchivo, string fuentes)
        {

            string listaCategoriasString = "";
            foreach (Categoria categoria in producto.Categorias)
            {
                listaCategoriasString += categoria.CategoriaID;

                if (categoria != producto.Categorias.Last())
                {
                    listaCategoriasString += ",";
                }
            }

            db.spVirtual_Insert(producto.Nombre, producto.CorreoCliente, producto.PrecioEstimado, producto.Condicion, producto.Descripcion,
                producto.ProductoImagen1, producto.ProductoImagen2, producto.ProductoImagen3, producto.Publicado, DateTime.Now ,derechosAutor, tipoArchivo, fuentes, DateTime.Now, listaCategoriasString);
        }

        public byte[] ConvertToByte(HttpPostedFileBase file2)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file2.InputStream);
            imageByte = rdr.ReadBytes((int)file2.ContentLength);
            return imageByte;
        }

        public void editeImagenProducto(HttpPostedFileBase Archivo_Imagen, Producto producto, byte[] imagenP, int n)
        {
            if (Archivo_Imagen != null) //Si edita la imagen
            {
                imagenP = ConvertToByte(Archivo_Imagen);
                switch (n)
                {
                    case 1:
                        if (imagenP.Length != 0) { producto.ProductoImagen1 = imagenP; }
                        break;

                    case 2:
                        if (imagenP.Length != 0) { producto.ProductoImagen2 = imagenP; }
                        break;

                    case 3:
                        if (imagenP.Length != 0) { producto.ProductoImagen3 = imagenP; }
                        break;

                    default:
                        break;
                }
            }
            else
            { // Si no selecciona foto nueva pero anteriormente había una entonces mantiene la información de la anterior
                switch (n)
                {
                    case 1:
                        producto.ProductoImagen1 = producto.ProductoImagen1;
                        break;

                    case 2:
                        producto.ProductoImagen2 = producto.ProductoImagen2;
                        break;

                    case 3:
                        producto.ProductoImagen3 = producto.ProductoImagen3;
                        break;

                    default:
                        break;
                }
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id, string correo)
        {
            ViewBag.Categoria = new SelectList(db.Categorias, "CategoriaId", "Nombre");
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            string correoActual = (string)HttpContext.Session["Correiro"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id, correo);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.correo = new SelectList(db.Clientes, "Correo", producto.CorreoCliente);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "ProductoID,CorreoCliente,Nombre,PrecioEstimado,Condicion,Descripcion,Publicado,ProductoImagen1,ProductoImagen2,ProductoImagen3,FechaRegistrado,FechaPublicado,Estado")] Producto producto, HttpPostedFileBase Archivo_Imagen1, HttpPostedFileBase Archivo_Imagen2, HttpPostedFileBase Archivo_Imagen3, string Publicado, FormCollection form)
        {
            Debug.WriteLine("Se edita: " + producto.FechaRegistrado);
            ViewBag.Categoria = new SelectList(db.Categorias, "CategoriaId", "Nombre");
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            string correoActual = (string)HttpContext.Session["Correiro"];

            if (Publicado == "true")
            {
                producto.Publicado = true;
                producto.FechaPublicado = DateTime.Now;
            }

            byte[] imagenP1 = { };
            byte[] imagenP2 = { };
            byte[] imagenP3 = { };

            editeImagenProducto(Archivo_Imagen1, producto, imagenP1, 1);
            editeImagenProducto(Archivo_Imagen2, producto, imagenP2, 2);
            editeImagenProducto(Archivo_Imagen3, producto, imagenP3, 3);

            string[] resultadosCheckboxes = form["checkboxCategoria"].Split(',');
            List<string> resultadosCheckboxesLista = resultadosCheckboxes.ToList();

            for (int i = 0; i < resultadosCheckboxesLista.Count; i++)
            {
                string resultado = resultadosCheckboxesLista[i];
                if (resultado == "true")
                {
                    resultadosCheckboxesLista.RemoveAt(i + 1);
                }
            }

            List<Categoria> listaCategorias = new List<Categoria>(db.Categorias);

            for (int i = 0; i < resultadosCheckboxesLista.Count; i++)
            {
                if (resultadosCheckboxesLista[i] == "true")
                {
                    listaCategorias[i].Productoes.Add(producto);
                    producto.Categorias.Add(listaCategorias[i]);
                }

            }


            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            ViewBag.CorreoCliente = new SelectList(db.Clientes, "user", producto.CorreoCliente);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id, string correo)
        {
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
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id, string correo)
        {
            var cookiecorreo = ControllerContext.HttpContext.Request.Cookies["user"];
            Producto producto = db.Productoes.Find(id, correo);
            producto.Estado = false;

            //db.Productoes.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insertar(Producto obj)
        {
            Producto objreg = new Producto();
            //string result = objreg.InsertProducto(obj);
            // ViewData["result"] = result;
            ModelState.Clear();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Publicar(string searchBy, string currentFilter, string searchString, int? page)
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
            //Producto producto;
            // Es para que me salgan solo mis productos, (yo siendo el usuario logueado)

            productos = productos.Where(p => p.Estado == true);

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



        /* Acciones a Usuario  */
        public ActionResult VerSignUp_Usuario()
        {
            return RedirectToAction("SignUp", "Usuario");
        }
        /* -------------------------------*/


        /*  Metodos para ProductoDelDia */
        public ActionResult VerIndex_ProductosDelDia()
        {
            return RedirectToAction("Index", "ProductosDelDia");
        }

        [Serializable]
        private class DbEntityValidationException : Exception
        {
            public DbEntityValidationException()
            {
            }

            public DbEntityValidationException(string message) : base(message)
            {
            }

            public DbEntityValidationException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected DbEntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public IEnumerable<object> EntityValidationErrors { get; internal set; }
        }
    }
}

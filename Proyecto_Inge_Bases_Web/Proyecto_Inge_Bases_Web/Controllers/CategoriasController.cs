using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Proyecto_Inge_Bases_Web.Models;

namespace Proyecto_Inge_Bases_Web.Controllers
{
    public class CategoriasController : Controller
    {
        private TempPIEntities db = new TempPIEntities();


        public ActionResult Index()
        {

            List<Categoria> ListaCategorias = db.Categorias.ToList();
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            return View(ListaCategorias);
        }


        public ActionResult Create(int? id)
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            Categoria subcategoria = new Categoria();//Se crea una nueva entidad de Categoria que se manda a la vista de Create
            subcategoria.CorreoUsuarioRegistrado = categoria.CorreoUsuarioRegistrado;
            subcategoria.CategoriaPadreID = categoria.CategoriaID;
            subcategoria.Nombre = null;
            subcategoria.EsCategoriaFisica = true;
            subcategoria.CorreoUsuarioRegistrado = admin.Correo;

            ViewBag.CategoriaPadreID = new SelectList(db.Categorias, "CategoriaID", "Nombre", categoria.CategoriaPadreID);
            ViewBag.CorreoUsuarioRegistrado = new SelectList(db.Registradoes, "Correo", "Correo");
            ViewBag.CrearPara = categoria.Nombre;
            return View(subcategoria);
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Create([Bind(Include = "CategoriaID,Nombre,CorreoUsuarioRegistrado,CategoriaPadreID,EsCategoriaFisica")] Categoria categoria)
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            void guardarCambios()
            {
                try
                {
                    db.Categorias.Add(categoria);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    categoria.CategoriaID++;
                    guardarCambios();

                }
            }
            // AS 17-6-2020 José Chaves Hurtado, Dayana Marín Mayorga
            // Story ID: Reb 22.5
            //Tarea técnica: Validación de datos
            if (ModelState.IsValid)
            {

                if (Regex.IsMatch(categoria.Nombre, "^[a-zA-ZáéíóúÁÉÍÓÚ]+$")) //Revisa si la casilla de nombre solamente contiene letras 
                {
                    guardarCambios();
                }
                else // Retorna un mensaje de error
                {
                    TempData["ErrorCrear"] = "El nombre solo puede contener letras.";
                }

                
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaPadreID = new SelectList(db.Categorias, "CategoriaID", "Nombre", categoria.CategoriaPadreID);
            ViewBag.CorreoUsuarioRegistrado = new SelectList(db.Registradoes, "Correo", "Correo", categoria.CorreoUsuarioRegistrado);
            return View(categoria);
        }
        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaPadreID = new SelectList(db.Categorias, "CategoriaID", "Nombre", categoria.CategoriaPadreID);
            ViewBag.CorreoUsuarioRegistrado = new SelectList(db.Registradoes, "Correo", "Correo", categoria.CorreoUsuarioRegistrado);
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public ActionResult Edit([Bind(Include = "CategoriaID,Nombre,CorreoUsuarioRegistrado,CategoriaPadreID,EsCategoriaFisica")] Categoria categoria)
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaPadreID = new SelectList(db.Categorias, "CategoriaID", "Nombre", categoria.CategoriaPadreID);
            ViewBag.CorreoUsuarioRegistrado = new SelectList(db.Registradoes, "Correo", "Correo", categoria.CorreoUsuarioRegistrado);
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            
            void borrarCategoria(Categoria categoriaABorrar)
            {
                var listaCategoriasABorrar = db.Categorias.Where(c => c.CategoriaPadreID == categoriaABorrar.CategoriaID);
                db.Categorias.Remove(categoriaABorrar);
                foreach(var cat in listaCategoriasABorrar)
                {
                    borrarCategoria(cat);
                }
            }
            borrarCategoria(categoria);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
      

        public ActionResult DeleteConfirmed(int? id)
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }
            Categoria categoria = db.Categorias.Find(id);
            //            Dev Dayana Marín Mayorga  Actividad Supervisada
            //            Dev José Chavés Hurtado  Actividad Supervisada
            //            REB22 / Subtarea: Consultar la cantidad de productos dada una categoría

            //var cantidad = categoria.Productoes.Count;
            //if (cantidad > 0)
            //{
            //    TempData["ErrorCantidadProductos"] = "La categoría que se desea eliminar aún tiene productos asociados.";
            //    return RedirectToAction("Index");
            //}
            //else
            //{

            // try catch
            db.Categorias.Remove(categoria);
            db.SaveChanges();
            return RedirectToAction("Index");
            // }

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
 
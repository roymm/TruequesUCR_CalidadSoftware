using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Inge_Bases_Web.Models;

namespace Proyecto_Inge_Bases_Web.Controllers
{ 
    public class EstadisticasController : Controller
    {

        private TempPIEntities db = new TempPIEntities();
        // GET: Estadisticas
        public ActionResult Index()
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }

            var usuario = from u in db.Calificaciones_Cliente
                          select u;
            usuario = usuario.OrderBy(u => u.Nombre);

            return View(usuario.ToList());
        }

        // GET: Estadisticas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Estadisticas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estadisticas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estadisticas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estadisticas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estadisticas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estadisticas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult TopUsuarios()
        {
            string correo = (string)HttpContext.Session["Correiro"];
            Administrador admin = db.Administradors.Find(correo);

            if (admin == null)
            {
                return RedirectToAction("IniciarSesion", "Registrado");
            }

            var usuario = from u in db.ProductosUsuarios
                          select u;
            usuario = usuario.OrderByDescending(u => u.CantidadProductos);
            usuario = usuario.Take(10);
            return View(usuario.ToList());
        }



    }

}
 
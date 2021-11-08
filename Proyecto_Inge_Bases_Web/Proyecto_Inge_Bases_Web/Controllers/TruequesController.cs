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
    public class TruequesController : Controller
    {
        private TempPIEntities db = new TempPIEntities();

        // GET: TruequesDummy
        public ActionResult Index()
        {
            var trueques = db.Trueques.Include(t => t.Producto).Include(t => t.Producto1);
            return View(trueques.ToList());
        }

        // GET: TruequesDummy/Details/5
        public ActionResult Details(int? p1, string c1, int p2, string c2)
        {
            if (p1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trueque trueque = db.Trueques.Find(p1, c1, p2, c2);
            if (trueque == null)
            {
                return HttpNotFound();
            }
            return View(trueque);
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
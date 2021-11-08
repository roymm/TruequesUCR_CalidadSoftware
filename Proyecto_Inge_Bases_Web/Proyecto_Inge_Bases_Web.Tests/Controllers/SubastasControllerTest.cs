using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto_Inge_Bases_Web.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;
using Proyecto_Inge_Bases_Web.Models;
using System.Web.UI;
using Proyecto_Inge_Bases_Web.ViewModels;

namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    [TestClass]
    public class SubastasControllerTest
    {
        [TestMethod]
        public void TestCreateNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.Create(1, "joalva18@gmail.com") as ViewResult;
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void TestCreateProductoNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.Create(2, "joalva18@gmail.com") as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestCreateUsuarioActualNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns(null);
            //controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(null);
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.Create(1, "joalva18@gmail.com") as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestCreateConSubListas()
        {
            SubastasController controller = new SubastasController();
            TempPIEntities db = new TempPIEntities();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            string fechaInicio = "12/12/2020";
            string fechaFin = "12/12/2020";
            string horaInicio = "03:55";
            string horaFin = "12:01";
            //Sublista sublista = new Sublista();
            var sublistas = from s in db.Sublistas
                            select s;
            sublistas = sublistas.Where(s => s.CorreoDueno == "joalva18@gmail.com");
            //meto en el selectList para mostrar las listas correctas en el view


            DateTime fechaP = DateTime.Parse(fecha);
            SubastaViewModel tmodel = new SubastaViewModel();
            Subasta subasta = new Subasta();
            tmodel.ProductoIDSubastado = 1;
            tmodel.FechaPublicado = fechaP;
            tmodel.PrecioMinimo = 210000;
            tmodel.FechInicio = fechaInicio;
            tmodel.FechFin = fechaFin;
            tmodel.HoraInicio = horaInicio;
            tmodel.HoraFin = horaFin;
            tmodel.sublistas = new List<SelectListItem>();
            tmodel.sublistas.Add(new SelectListItem { Text = "0", Value = "Público" });
            tmodel.sublistas.Add(new SelectListItem { Text = "0", Value = "Mis Amigos" });
            foreach (var item in sublistas)
            {
                tmodel.sublistas.Add(new SelectListItem { Text = item.id.ToString(), Value = item.NombreSublista });
            }
            var result = (RedirectToRouteResult)controller.Create(tmodel);
            result.RouteValues["action"].Equals("Index");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            /* ViewResult result = controller.Create(tmodel) as ViewResult;
             Assert.AreEqual("", result.ViewName);

             var result = (RedirectToRouteResult)controller.Edit(tmodel);
             result.RouteValues["action"].Equals("Index");
             Assert.AreEqual("Index", result.RouteValues["action"]);*/
        }

        [TestMethod]
        public void TestCreateView()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.Create(1, "joalva18@gmail.com") as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestCreate()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            string fechaInicio = "12/12/2020";
            string fechaFin = "12/12/2020";
            string horaInicio = "03:55";
            string horaFin = "12:01";
            DateTime fechaP = DateTime.Parse(fecha);
            SubastaViewModel tmodel = new SubastaViewModel();
            tmodel.ProductoIDSubastado = 1;
            tmodel.FechaPublicado = fechaP;
            tmodel.PrecioMinimo = 210000;
            tmodel.FechInicio = fechaInicio;
            tmodel.FechFin = fechaFin;
            tmodel.HoraInicio = horaInicio;
            tmodel.HoraFin = horaFin;
            //tmodel.sublistas
            ViewResult result = controller.Create(tmodel) as ViewResult;
            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod]
        public void TestCreateIsNotNull() //este es para el create con bind
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            string fechaInicio = "12/12/2020";
            string fechaFin = "12/12/2020";
            string horaInicio = "03:55";
            string horaFin = "12:01";
            DateTime fechaP = DateTime.Parse(fecha);
            SubastaViewModel tmodel = new SubastaViewModel();
            tmodel.ProductoIDSubastado = 1;
            tmodel.FechaPublicado = fechaP;
            tmodel.PrecioMinimo = 210000;
            tmodel.FechInicio = fechaInicio;
            tmodel.FechFin = fechaFin;
            tmodel.HoraInicio = horaInicio;
            tmodel.HoraFin = horaFin;
            ViewResult result = controller.Create(tmodel) as ViewResult;
            Assert.IsNotNull(result);

        }



        [TestMethod]
        public void TestDeleteNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Delete(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void TestDeleteNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "24-06-2020 08:25:19.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Delete(1, "juangarro05@gmail.com", fechaT) as ViewResult;
            Assert.IsNull(result);

        }

        [TestMethod]
        public void TestDeleteView()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Delete(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestDeleteViewData()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Delete(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Subasta subasta = (Subasta)result.ViewData.Model;
            Assert.AreEqual(210000, subasta.PrecioMinimo);

        }

        [TestMethod]
        public void TestDeleteConfirmed()
        {
            SubastasController controller = new SubastasController();
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaI = DateTime.Parse(fecha);


            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            var result = controller.DeleteConfirmed(1, "joalva18@gmail.com", fechaI);
            Assert.IsNotNull(result);
            //result.RouteValues["action"].Equals("Index");
            //Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        [TestMethod]
        public void TestOfertarEnSubasta()
        {
            SubastasController controller = new SubastasController();
            SubastaViewModel oferta = new SubastaViewModel();
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaI = DateTime.Parse(fecha);
            //oferta. = "mariovargasc97@gmail.com";
            oferta.ProductoIDSubastado = 1;
            oferta.CorreoSubastador = "joalva18@gmail.com";
            oferta.FechaPublicado = fechaI;
            oferta.PrecioOfrecido = 30000;



            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("mariovargasc97@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("mariovargasc97@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.OfertarEnSubasta(oferta) as ViewResult;
            Assert.IsNull(result);

        }

        [TestMethod]
        public void TestEditarOfertar()
        {
            SubastasController controller = new SubastasController();
            Relacion_ClienteOfertaEnSubasta oferta = new Relacion_ClienteOfertaEnSubasta();
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaI = DateTime.Parse(fecha);
            oferta.CorreoOfertador = "mariovargasc97@gmail.com";
            oferta.ProductoIDSubastado = 1;
            oferta.CorreoSubastador = "joalva18@gmail.com";
            oferta.FechaPublicado = fechaI;
            oferta.PrecioOfrecido = 35000;



            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.editarOferta(oferta) as ViewResult;
            Assert.IsNull(result);

        }

        [TestMethod]
        public void TestDetailsCorreoActualNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns(null);
            //controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(null);
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Details(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDetailsProductoNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Details(null, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDetailsOfertaMaximaNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("mariovargasc97@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("mariovargasc97@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "15-07-2020 08:41:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Details(27, "mariovargasc97@gmail.com", fechaT) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetailsNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "14-07-2020 00:34:21.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Details(15, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetailsNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "24-06-2020 08:25:19.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Details(1, "juangarro05@gmail.com", fechaT) as ViewResult;
            Assert.IsNull(result);

        }

        [TestMethod]
        public void TestDetailsView()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Details(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestDetailsViewData()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Details(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Subasta subasta = (Subasta)result.ViewData.Model;
            Assert.AreEqual(210000, subasta.PrecioMinimo);
        }

        [TestMethod]
        public void TestEditNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Edit(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEditNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "24-06-2020 08:25:19.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Edit(1, "juangarro05@gmail.com", fechaT) as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEditView()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Edit(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }



        //Edit con bind

        [TestMethod]
        /// <summary>Test stub for Edit(Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)</summary>
        public void TestEdit()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            string fechaInicio = "12/12/2020";
            string fechaFin = "12/12/2020";
            string horaInicio = "03:55";
            string horaFin = "12:01";
            DateTime fechaP = DateTime.Parse(fecha);
            SubastaViewModel tmodel = new SubastaViewModel();
            tmodel.ProductoIDSubastado = 1;
            tmodel.FechaPublicado = fechaP;
            tmodel.PrecioMinimo = 210000;
            tmodel.FechInicio = fechaInicio;
            tmodel.FechFin = fechaFin;
            tmodel.HoraInicio = horaInicio;
            tmodel.HoraFin = horaFin;
            var result = (RedirectToRouteResult)controller.Edit(tmodel);
            result.RouteValues["action"].Equals("Index");
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        /// <summary>Test stub for Edit(Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)</summary>
        public void TestEditIsNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            string fechaInicio = "12/12/2020";
            string fechaFin = "12/12/2020";
            string horaInicio = "03:55";
            string horaFin = "12:01";
            DateTime fechaP = DateTime.Parse(fecha);
            SubastaViewModel tmodel = new SubastaViewModel();
            tmodel.ProductoIDSubastado = 1;
            tmodel.FechaPublicado = fechaP;
            tmodel.PrecioMinimo = 210000;
            tmodel.FechInicio = fechaInicio;
            tmodel.FechFin = fechaFin;
            tmodel.HoraInicio = horaInicio;
            tmodel.HoraFin = horaFin;
            var result = (RedirectToRouteResult)controller.Edit(tmodel);
            result.RouteValues["action"].Equals("Index");
            Assert.IsNotNull(result);


            //ActionResult result = controller.Edit
            //  (producto, null, null, null, "1");
            //Assert.AreNotEqual("Index", result);
            // TODO: add assertions to method ProductoControllerTest.EditTest(ProductoController, Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)
        }



        [TestMethod]
        public void TestIndexNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexUsuarioNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns(null);
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "30-05-2020 13:10:20.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestIndexView()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "30-05-2020 13:10:20.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestListaProductosNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.ListaProductos(null, null, null, 1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestListaProductosNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.ListaProductos(null, null, "Play Station 4 Como Nuevo!", 1) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestListaProductosView()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.ListaProductos(null, null, null, 1) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestmisSubastasNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.misSubastas() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestmisSubastasView()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.misSubastas() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestOfertarEnSubastaNotNull()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.OfertarEnSubasta(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestOfertarEnSubastaNotNullSinEditar()
        {
            SubastasController controller = new SubastasController();
            SubastaViewModel oferta = new SubastaViewModel();
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaI = DateTime.Parse(fecha);
            //oferta. = "mariovargasc97@gmail.com";
            oferta.ProductoIDSubastado = 1;
            oferta.CorreoSubastador = "joalva18@gmail.com";
            oferta.FechaPublicado = fechaI;
            oferta.PrecioOfrecido = 30000;



            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("richixalfaro@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("richixalfaro@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.OfertarEnSubasta(oferta) as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestOfertarEnSubastaView()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            string fecha = "11-07-2020 23:15:32.000";
            DateTime fechaT = DateTime.Parse(fecha);
            ViewResult result = controller.OfertarEnSubasta(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestEnviarCorreo()
        {
            SubastasController controller = new SubastasController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.EnviarCorreo("juangarro05@gmail.com", "Ha recibido una oferta de subasta") as ViewResult;
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void TestRecursoNoEncontrado()
        {
            SubastasController controller = new SubastasController();
            ViewResult result = controller.RecursoNoEncontrado() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}

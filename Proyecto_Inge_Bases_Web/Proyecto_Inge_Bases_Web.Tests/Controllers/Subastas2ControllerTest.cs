using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto_Inge_Bases_Web.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;
using Proyecto_Inge_Bases_Web.Models;

namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    [TestClass]
    public class Subastas2ControllerTest
    {

        [TestMethod]
        public void TestCreateNotNull()
        {
            Subastas2Controller controller = new Subastas2Controller();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.Create(2, "mariovargasc97@gmail.com", null, "Botas de vestir cafe de cuero", "Nuevo", 25000) as ViewResult;
            Assert.IsNotNull(result);
           
        }

        [TestMethod]
        public void TestCreateView()
        {
            Subastas2Controller controller = new Subastas2Controller();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.Create(2, "mariovargasc97@gmail.com", null, "Botas de vestir cafe de cuero", "Nuevo", 25000) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestDeleteNotNull()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Delete(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNotNull(result);
            
        }

        [TestMethod]
        public void TestDeleteView()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Delete(1, "joalva18@gmail.com", fechaT) as ViewResult; 
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestDeleteViewData()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Delete(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Subasta subasta = (Subasta)result.ViewData.Model;
            Assert.AreEqual(25000, subasta.PrecioMinimo);
        }

        [TestMethod]
        public void TestDetailsNotNull()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Details(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetailsView()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Details(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestDetailsViewData()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Details(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Subasta subasta = (Subasta)result.ViewData.Model;
            Assert.AreEqual(25000, subasta.PrecioMinimo);
        }

        [TestMethod]
        public void TestEditNotNull()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Edit(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEditView()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Edit(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestEditViewData()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            ViewResult result = controller.Edit(1, "joalva18@gmail.com", fechaT) as ViewResult;
            Subasta subasta = (Subasta)result.ViewData.Model;
            Assert.AreEqual(25000, subasta.PrecioMinimo);
        }

        [TestMethod]
        public void TestIndexNotNull()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexView()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            Subastas2Controller controller = new Subastas2Controller();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.ListaProductos(null,null,null,1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestListaProductosView()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            Subastas2Controller controller = new Subastas2Controller();
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
            Subastas2Controller controller = new Subastas2Controller();
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
            Subastas2Controller controller = new Subastas2Controller();
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
            string fechaInicio = "2020-05-30 13:10:20.0000000";
            DateTime fechaI = DateTime.Parse(fecha);
            string fechaFinal = "2020-06-01 13:10:20.0000000";
            DateTime fechaF = DateTime.Parse(fecha);
            ViewResult result = controller.OfertarEnSubasta(2, "mariovargasc97@gmail.com", fechaT, "Botas de vestir cafe de cuero", "Nuevo", "Cuero cubano, 100% real.  Ajuste ergonomico en el pie para caminar largas distancias.   Talla: 36", 25000, fechaI, fechaF) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestOfertarEnSubastaView()
        {
            Subastas2Controller controller = new Subastas2Controller();
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
            string fechaInicio = "2020-05-30 13:10:20.0000000";
            DateTime fechaI = DateTime.Parse(fecha);
            string fechaFinal = "2020-06-01 13:10:20.0000000";
            DateTime fechaF = DateTime.Parse(fecha);
            ViewResult result = controller.OfertarEnSubasta(2, "mariovargasc97@gmail.com", fechaT, "Botas de vestir cafe de cuero", "Nuevo", "Cuero cubano, 100% real.  Ajuste ergonomico en el pie para caminar largas distancias.   Talla: 36", 25000, fechaI, fechaF) as ViewResult;
             Assert.AreEqual("", result.ViewName);
        }

        

    }
}

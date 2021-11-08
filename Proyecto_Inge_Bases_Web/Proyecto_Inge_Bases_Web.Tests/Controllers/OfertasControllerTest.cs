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
    public class OfertasControllerTest
    {
        [TestMethod]
        public void TestIndexNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index("","","",1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestDetailsNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Details(0, "joalva18@gmail.com") as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexSearchStringNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index("", "", null, 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexSearchStringNotEmptyNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index("", "", "Play", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexSearchByNameAscNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index("Name asc", "", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexSearchByNameDescNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index("Price desc", "", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexSearchByPriceNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index("Price", "", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexCurrentFilterNotNull()
         {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index("", "Play", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]    
        public void TestIndexPaginationNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index("", "", "", 2) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMisProductosModalNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.MisProductosModal("", "", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void TestMisProductosModalSearchStringNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.MisProductosModal("", "", null, 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMisProductosModalSearchStringNotEmptyNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.MisProductosModal("", "", "Play", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMisProductosModalSearchByNameAscNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.MisProductosModal("Name asc", "", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMisProductosModalSearchByNameDescNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.MisProductosModal("Price desc", "", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMisProductosModalSearchByPriceNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.MisProductosModal("Price", "", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMisProductosModalCurrentFilterNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.MisProductosModal("", "Play", "", 1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMisProductosModalPaginationNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.MisProductosModal("", "", "", 2) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


        /*
         * Historia ID: NEO-23
         * Driver: Jorge
         * Navigators: Maria y Yulian
         * Task: 23.5 Testing y Correcciones
         * Descripcion: Se realiza un test para el view DetailsOfertas
         */
        [TestMethod]
        public void TestDetailsOfertasNotNull()
        {
            // Arrange
            OfertasController controller = new OfertasController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("correo@prueba.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.DetailsOferta(0, "joalva18@gmail.com") as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        /*
         * Historia ID: NEO-23
         * Driver: Jorge
         * Navigators: Maria y Yulian
         * Task: 23.5 Testing y Correcciones
         * Descripcion: Se realiza un test para el view DetailsOfertas
         */

    }
}

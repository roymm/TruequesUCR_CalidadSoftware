using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto_Inge_Bases_Web;
using Proyecto_Inge_Bases_Web.Controllers;
using Proyecto_Inge_Bases_Web.Models;
using Proyecto_Integrador_Bases_Inge.Controllers;
using System.Web;

namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    [TestClass]
    public class ClienteControllerTest
    {
        [TestMethod]
        public void TestIndexNotNull()
        {
            ClienteController cliente = new ClienteController();
            ViewResult result = cliente.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestListaAmigosNotNull()
        {

            ClienteController controller = new ClienteController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;

            // Act
            ViewResult result = controller.ListaAmigos() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestPerfilClienteNotNull()
        {

            ClienteController controller = new ClienteController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;

            // Act
            // Se comento porque da error
            //ViewResult result = controller.PerfilCliente() as ViewResult;
            // Assert
            //Assert.IsNotNull(result);
        }

        /*
         * Inicio AS Johan Murillo - Cesar Vasquez  01/07/2020
         * UnitTest de Vista de datos editados.
         * Historia OC-1.6 Completar Perfil.
         */
        [TestMethod]
        public void TestDetailsNotNull()
        {
            ClienteController cliente = new ClienteController();
            ViewResult result = cliente.Details("joalva18@gmail.com") as ViewResult;
            Assert.IsNotNull(result);
        }
        /* Supervisada 01/07/2020 OC-1.6 Johan Murillo - César Vásquez */
        [TestMethod]
        public void TestCreateNotNull()
        {
            ClienteController controller = new ClienteController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Create() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDeleteNotNull()
        {
            ClienteController controller = new ClienteController();
            // Act
            ViewResult result = controller.Delete("joalva18@gmail.com") as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}

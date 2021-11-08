using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto_Inge_Bases_Web.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;
using Proyecto_Inge_Bases_Web.Models;


/*
    REB-19
    Inicio actividad supervisada
    Driver: Manuel Fernandez
    Navigator: Mario Viquez
 */



namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    [TestClass]
    public class DashboardControllerTest
    {

        [TestMethod]
        public DashboardController Login() 
        {

            DashboardController controller = new DashboardController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("isaac@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("isaac@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;

            return controller;

        }

        [TestMethod]
        public void TestIndexNotNull()
        {

            DashboardController controller = Login();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void TestEdadesClientesNotNull()
        {

            DashboardController controller = Login();

            // Act
            ViewResult result = controller.EdadesClientes() as ViewResult;
            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void TestCalificacionesUsuariosNotNull()
        {

            DashboardController controller = Login();

            // Act
            ViewResult result = controller.CalificacionesUsuarios() as ViewResult;
            // Assert
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void TestEdadesClientesViewData()
        {

            DashboardController controller = Login();

            // Act
            ViewResult result = controller.EdadesClientes() as ViewResult;
            List<Calificaciones_Cliente> clientes = (List<Calificaciones_Cliente>)result.ViewData.Model;
            // Assert
            Assert.AreEqual(26, clientes.Count);

        }

        [TestMethod]
        public void TestCalificacionesUsuarioViewData()
        {

            DashboardController controller = Login();

            // Act
            ViewResult result = controller.CalificacionesUsuarios() as ViewResult;
            List<Calificaciones_Cliente> clientes = (List<Calificaciones_Cliente>)result.ViewData.Model;
            // Assert
            Assert.AreEqual(26, clientes.Count);

        }

    }
}

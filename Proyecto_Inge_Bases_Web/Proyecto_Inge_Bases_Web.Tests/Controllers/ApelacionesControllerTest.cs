using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto_Inge_Bases_Web.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;

namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    [TestClass]
    public class ApelacionesControllerTest
    {
        [TestMethod]
        public void TestIndexNotNull()
        {
            ApelacionesController controller = new ApelacionesController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("isaac@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("isaac@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;

            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestApelacionExitosaNotNull()
        {
            ApelacionesController controller = new ApelacionesController();
            ViewResult result = controller.Apelacion_Exitosa() as ViewResult;
            Assert.IsNotNull(result, "Null");
            Assert.AreEqual("Apelacion_Exitosa", result.ViewName, "Apelacion_Exitosa");
        }

        [TestMethod]
        public void TestErrorApelacionNotNull()
        {
            ApelacionesController controller = new ApelacionesController();
            ViewResult result = controller.Error_Apelacion() as ViewResult;
            Assert.IsNotNull(result, "Null");
            Assert.AreEqual("Error_Apelacion", result.ViewName, "Error_Apelacion");
        }

        [TestMethod]
        public void TestRealizar_ApelacionNotNull() 
        {
            ApelacionesController controller = new ApelacionesController();
            ViewResult result = controller.Realizar_Apelacion("correo") as ViewResult;
            Assert.IsNotNull(result, "Null");
            Assert.AreEqual("Error_Apelacion", result.ViewName, "Error_Apelacion");
        }

    }
}

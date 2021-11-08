using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto_Inge_Bases_Web.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;

namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    [TestClass]
    public class SolicitudControllerTest
    {
        [TestMethod]
        public void TestIndexNotNull()
        {

            // Arrange
            SolicitudController controller = new SolicitudController();

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
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}

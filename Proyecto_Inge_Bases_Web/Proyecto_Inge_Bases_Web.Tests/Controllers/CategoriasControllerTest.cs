using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto_Inge_Bases_Web.Controllers;
using Proyecto_Inge_Bases_Web.Models;
using System.Web.Mvc;
using System.Web;
using Moq;

namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    // AS 1-7-2020 José Chaves Hurtado, Dayana Marín Mayorga
    // Story ID: Reb 22
    //Tarea técnica: Proyecto de pruebas
    [TestClass]
    public class CategoriasControllerTest
    {
        [TestMethod]
        public void TestIndexNotNull()
        {
            CategoriasController controller = new CategoriasController();

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
        public void CreateNotNull()
        {
            CategoriasController controller = LoginAdministrador();


            // Act
            ViewResult result = controller.Create(0) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void EditNotNull()
        {
            CategoriasController controller = LoginAdministrador();
            // Act
            ViewResult result = controller.Edit(0) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexViewData()
        {
            CategoriasController controller = LoginAdministrador();
            ViewResult result = controller.Index() as ViewResult;
            List<Categoria> categorias = (List<Categoria>)result.ViewData.Model;
            Assert.AreEqual(21, categorias.Count);
        }
        [TestMethod]
        public void TestEditViewData()
        {
            CategoriasController controller = LoginAdministrador();
            ViewResult result = controller.Edit(1) as ViewResult;
            Categoria categoria = (Categoria)result.ViewData.Model;
            Assert.AreEqual("Entretenimiento", categoria.Nombre);
        }

        [TestMethod]
        public CategoriasController LoginAdministrador()
        {
            CategoriasController controller = new CategoriasController();

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

    }

    
}

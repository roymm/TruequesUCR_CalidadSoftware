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
    /// <summary>
    /// Summary description for NotificationsControllerTest
    /// </summary>
    [TestClass]
    public class NotificationsControllerTest
    {
        public NotificationsControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestRecibeNotNull()
        {

            // Arrange
            NotificationsController controller = new NotificationsController();

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
            ViewResult result = controller.Recibe("", "", "", null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestSolicitudNotNull()
        {

            // Arrange
            NotificationsController controller = new NotificationsController();

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
            ViewResult result = controller.Solicitud(It.IsAny<int>() ) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


        //NO HECHO TODAVIA:

        [TestMethod]
        public void TestDetallesProductosOfertadosNotNull()
        {

            // Arrange
            NotificationsController controller = new NotificationsController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.DetallesProductosOfertados(5, "richixalfaro@gmail.com", 18, "juangarro05@gmail.com") as ViewResult;
            Assert.IsNotNull(result);


            // Act
            // Marco lo comento porque da error
            //ViewResult result = controller.DetallesProductosOfertados(It.IsAny<int>(), It.IsAny<string>()) as ViewResult;
            // Assert
            //Assert.IsNotNull(result);
        }


        //ID de la historia:NEO-23
        //Tarea: 23.5
        //Se crearon pruebas de unidad para los metodos del controlador EnviarCorreo,delete y MisTrueques 
        //Participantes:
        //Navegator: Jorge Chavarria,Yulian Loaiza
        //Driver: Maria Peraza
        [TestMethod]
        public void TestEnviarCorreoNotNull()
        {
            NotificationsController controller = new NotificationsController();
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
        public void TestDeleteNull()
        {
            NotificationsController controller = new NotificationsController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.Delete(2, "mariovargasc97@gmail.com") as ViewResult;
            Assert.IsNull(result);

        }

        [TestMethod]
        public void TestMisTruequesNotNull()
        {
            NotificationsController controller = new NotificationsController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.MisTrueques() as ViewResult;
            Assert.IsNotNull(result);

        }
    }
}

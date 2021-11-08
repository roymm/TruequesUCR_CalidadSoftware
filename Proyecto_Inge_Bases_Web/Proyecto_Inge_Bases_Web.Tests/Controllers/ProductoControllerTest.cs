using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto_Inge_Bases_Web.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;
using Proyecto_Inge_Bases_Web.Models;
using System.Web.Routing;


//Inicio AS 01/07/2020 Unit Testing en Pair Programming 
//Devs: Juan Garro y Marco Ferraro
namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    /// <summary>
    /// Summary description for ProductoControllerTest
    /// </summary>
    [TestClass]
    public class ProductoControllerTest
    {
        public ProductoControllerTest()
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
        public void TestIndexNotNull()
        {

            // Arrange
            ProductoController controller = new ProductoController();

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
            ViewResult result = controller.Index("", "", "", null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndex()
        {

            // Arrange
            ProductoController controller = new ProductoController();

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
            ViewResult result = controller.Index("", "", "", null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexSearchString()
        {

            // Arrange
            ProductoController controller = new ProductoController();

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
            ViewResult result = controller.Index("Price desc", "", "Prueba", null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestCreateNotNull()
        {

            // Arrange
            ProductoController controller = new ProductoController();

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
            ViewResult result = controller.Create() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetailsNotNull()
        {

            // Arrange
            ProductoController controller = new ProductoController();

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
            ViewResult result = controller.Details(1, "joalva18@gmail.com") as ViewResult; //Test integración al correr el post deployment.
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        /// <summary>Test stub for Edit(Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)</summary>
        public void TestEditNotNull()
        {
            ProductoController controller = new ProductoController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;


            ActionResult result = controller.Edit
                                        (1, "joalva18@gmail.com"); //Utilizo el de Johan como prueba
            Assert.IsNotNull(result);
        }

        [TestMethod] //Ocupa que se le pase el ID del productoInsertado en la Prueba para correr
        /// <summary>Test stub for Edit(Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)</summary>
        public void EditTest()
        {
            ProductoController controller = new ProductoController();
            Producto producto = new Producto(); // Creo mock de prod en la base de datos
            producto.CorreoCliente = ("joalva18@gmail.com");
            producto.PrecioEstimado = 200000;
            producto.Nombre = ("Test!");
            producto.Condicion = ("Usado");
            producto.Descripcion = ("TestStub");           

            producto.ProductoID = -8; //<-----Modificar para el de prueba

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;

            ActionResult result = controller.Edit
                                        (producto, null, null, null, "true");
            Assert.AreNotEqual("Index",  result);
            // TODO: add assertions to method ProductoControllerTest.EditTest(ProductoController, Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)
        }

        [TestMethod]
        /// <summary>Test stub for Edit(Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)</summary>
        public void CreateTest()
        {
            FormCollection form = new FormCollection();//Populo form
            form.Add("Derechos", "");
            form.Add("TipoArchivo", "");
            form.Add("Fuentes", "");
            form.Add("TipoProducto", "1");
            form.Add("PrecioEstimado", "80000");            
            
            ProductoController controller = new ProductoController();
            Producto producto = new Producto(); // Creo mock de prod en la base de datos            
            producto.Nombre = ("Test");
            producto.Condicion = ("Nuevo");
            producto.PrecioEstimado = 30000;
            producto.CorreoCliente= ("joalva18@gmail.com");
            producto.FechaPublicado = DateTime.Now;

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com"); //Efectuamos prueba con joan
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;

             var result= (RedirectToRouteResult)controller.Create
                                         (producto, null, null, null, form); //Sin imagenes
       
            result.RouteValues["action"].Equals("Index");
            //result.RouteValues["controller"].Equals("Home");

            Assert.AreEqual("Index", result.RouteValues["action"]);
            // Assert.AreEqual("Home", action.RouteValues["controller"]);

        }

        [TestMethod]
        /// <summary>Test stub for Edit(Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)</summary>
        public void TestDelete()
        {
            ProductoController controller = new ProductoController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;


            ActionResult result = controller.Delete
                                        (1, "joalva18@gmail.com"); //Utilizo el de Johan como prueba
            Assert.IsNotNull(result);
        }

        [TestMethod]//Ocupa que se le pase el ID del productoInsertado en la Prueba para correr
        /// <summary>Test stub for Edit(Producto, HttpPostedFileBase, HttpPostedFileBase, HttpPostedFileBase, String)</summary>
        public void TestDeleteConfirmed()
        {
            ProductoController controller = new ProductoController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;


            ActionResult result = controller.DeleteConfirmed
                                        (-8, "joalva18@gmail.com"); //<-----Modificar para el de prueba
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDispose() 
        {
           
        }
        

        // Empiezan Pruebas de Integracion
        [TestMethod]
        public void TestDetailsViewData()
        {
            ProductoController controller = new ProductoController();

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
            ViewResult result = controller.Details(0, "joalva18@gmail.com") as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetailsViewData2()
        {
            ProductoController controller = new ProductoController();

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
            ViewResult result = controller.Details(1, "mariovargasc97@gmail.com") as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexViewData()
        {
            ProductoController controller = new ProductoController();

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["Correiro"]).Returns("isaac@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("isaac@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("user"));
            controllerContext.SetupGet(p => p.HttpContext.Request.Cookies).Returns(cookies);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(cookies);
            controller.ControllerContext = controllerContext.Object;

            ViewResult result = controller.Index(null, null, "Play", 0) as ViewResult;
            List<Producto> productos = (List<Producto>)result.ViewData.Model;
            Assert.AreEqual(1, productos.Count);
        }

        [TestMethod]
        public void TestInsertar() 
        {
            ProductoController controller = new ProductoController();
            ActionResult result = controller.Insertar();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestInsertProductoVirtual()
        {
            ProductoController controller = new ProductoController();
            ActionResult result = controller.Insertar();
            Assert.IsNotNull(result);
        }

     

    }
}

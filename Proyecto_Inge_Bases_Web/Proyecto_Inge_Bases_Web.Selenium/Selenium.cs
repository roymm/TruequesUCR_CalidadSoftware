using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace Proyecto_Inge_Bases_Web.Selenium
{
    [TestClass]
    public class Selenium
    {
        IWebDriver driver;

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit();
        }

        [TestMethod]
        public void PruebaIngresoChrome()
        {
            ///Arrange
            /// Crea el driver de Chrome
            driver = new ChromeDriver();
            /// Pone la pantalla en full screen
            driver.Manage().Window.Maximize();
            ///Act
            /// Se va a la URL de la aplicacion
            driver.Url = "https://localhost:44324/home";
            ///Assert
        }
        [TestMethod]
        //Aqui comienza la actividad supervisada de pair programing en pruebas de unidad y selenium
        //driver : Joshua Ramirez Alfaro B76136 , se hace con Daniel Sancho Varela
        //historia trabajada con prueba : OC 3.1 Editar Perfil
        //tareas realizadas: prueba con selenium de entrar a editar perfil y cambiar el segundo apellido y guardar cambios
        public void PruebaEditarSegundoApellido()
        {
           
            var driver = new ChromeDriver();
         
            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/IniciarSesion");

            var loginBox = driver.FindElement(By.Id("correo1"));
            loginBox.SendKeys("joalva18@gmail.com");
            
            var pwBox = driver.FindElement(By.Id("Contraseña1"));
            pwBox.SendKeys("Pepe1515");
            driver.FindElement(By.Name("submit")).Click(); 
            driver.Navigate().GoToUrl("https://localhost:44324/Cliente/EditarPerfil");
            var cambioSegundoApellido = driver.FindElement(By.Name("Apellido2"));
            cambioSegundoApellido.Clear();
            cambioSegundoApellido.SendKeys("Bejarano");
            driver.FindElement(By.Name("botonf")).Click();
         
            driver.Quit();

        }
        [TestMethod]
        public void PruebaAgregarSublista()
        {

            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/IniciarSesion");

            var loginBox = driver.FindElement(By.Id("correo1"));
            loginBox.SendKeys("joalva18@gmail.com");

            var pwBox = driver.FindElement(By.Id("Contraseña1"));
            pwBox.SendKeys("Pepe1515");
            driver.FindElement(By.Name("submit")).Click();
            driver.Navigate().GoToUrl("https://localhost:44324/Cliente/ListaDeAmigosSublistas");
            var cambioSegundoApellido = driver.FindElement(By.Name("agregarSublista"));
            cambioSegundoApellido.Clear();
            cambioSegundoApellido.SendKeys("Crossfiteros");
            driver.FindElement(By.Name("botonSublista")).Click();

            driver.Quit();

        }
        [TestMethod]
        public void PruebaAgregarDireccionExacta()
        {
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/IniciarSesion");

            var loginBox = driver.FindElement(By.Id("correo1"));
            loginBox.SendKeys("joalva18@gmail.com");

            var pwBox = driver.FindElement(By.Id("Contraseña1"));
            pwBox.SendKeys("Pepe1515");
            driver.FindElement(By.Name("submit")).Click();
            driver.Navigate().GoToUrl("https://localhost:44324/Cliente/EditarPerfil");
            var cambioSegundoApellido = driver.FindElement(By.Name("Direccion"));
            cambioSegundoApellido.Clear();
            cambioSegundoApellido.SendKeys("Donde Lupe");
            driver.FindElement(By.Name("botonf")).Click();

            driver.Quit();



        }
        /*Inicio de AS 1/7/20 MarioVargas_B67454 & RicardoAlfaro_B70257
         Historia Usuario: OC - 1.2 & 1.3
             */

        /*Prueba de Selenium (3 capas) para RegistrarUnUsuario*/
        [TestMethod]
        public void PruebaRegistroUsuario()
        {
            //Iniciar el navegador
            PruebaIngresoChrome(); 
            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/Registro");
            //Insertar valor del correo
            var correo = driver.FindElement(By.Id("email"));
            correo.SendKeys("juancito02@gmail.com");
            //Insertar valor de contraseña
            var password = driver.FindElement(By.Id("Contraseña"));
            password.SendKeys("Pepe1515");
            //Insertar valor de confirmarContraseña
            var password2 = driver.FindElement(By.Id("ConfirmarContra"));
            password2.SendKeys("Pepe1515");
            //Click boton aceptar terminos y condiciones
            driver.FindElement(By.Name("checkbox")).Click();
            //Click en el boton de registrarse
            driver.FindElement(By.Name("submit")).Click();
            //Terminar driver
            TearDown();

        }

        /*Prueba de Selenium (3 capas) para IniciarSesion*/
        [TestMethod]
        public void PruebaIniciarSesion()
        {
            //Iniciar el navegador
            PruebaIngresoChrome();
            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/IniciarSesion");
            //Insertar valor del correo
            var correo = driver.FindElement(By.Id("correo1"));
            correo.SendKeys("joalva18@gmail.com");
            //Insertar valor de contraseña
            var password = driver.FindElement(By.Id("Contraseña1"));
            password.SendKeys("Pepe1515");
            //Click en el boton de iniciar sesion
            driver.FindElement(By.Name("submit")).Click();
            //Terminar driver
            TearDown();

        }

        /* Supervisada 01/07/2020 OC-1.6 Johan Murillo - César Vásquez
         * Prueba de selenium que activa el checkbox de desactivar notificaciones
         */
        [TestMethod]
        public void PruebasNotificaciones()
        {
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/IniciarSesion");

            var loginBox = driver.FindElement(By.Id("correo1"));
            loginBox.SendKeys("joalva18@gmail.com");

            var pwBox = driver.FindElement(By.Id("Contraseña1"));
            pwBox.SendKeys("Pepe1515");


            driver.FindElement(By.Name("submit")).Click();

            driver.Navigate().GoToUrl("https://localhost:44324/Cliente/EditarPerfil");

            driver.FindElement(By.Name("checkbox1")).Click();


            driver.FindElement(By.Name("botonf")).Click();

            driver.Quit();
        }
        //AS 1 Junio Historias:NEO-23 
        //Avanzo en el testing de ambas historias tecnicas: 23.5 
        //Participantes: Yulian Loaiza como driver Navigators: Maria Peraza, Jorge Chavarria
        //Contribución: 2 métodos de web driver para Notifications. Métodos PruebaAssertNotificationsButton() y PruebaAssertNotificationsTitulo()
        [TestMethod]
        public void PruebaAssertNotificationsButton()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44324/ProductosDelDia");
            driver.Manage().Window.Size = new System.Drawing.Size(1552, 880);
            driver.FindElement(By.LinkText("Iniciar Sesión")).Click();
            driver.FindElement(By.Id("correo1")).Click();
            driver.FindElement(By.Id("correo1")).SendKeys("joalva18@gmail.com");
            driver.FindElement(By.Id("Contraseña1")).Click();
            driver.FindElement(By.Id("Contraseña1")).SendKeys("Pepe1515");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.CssSelector(".navbar-toggler-icon")).Click();
            Assert.AreEqual(driver.FindElement(By.LinkText("Notificaciones")).Text, "Notificaciones"); //Boton del side menu "Notificaciones"

        }

        [TestMethod]
        public void PruebaAssertNotificationsTitulo()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44324/ProductosDelDia");
            driver.Manage().Window.Size = new System.Drawing.Size(1552, 880);
            driver.FindElement(By.LinkText("Iniciar Sesión")).Click();
            driver.FindElement(By.Id("correo1")).Click();
            driver.FindElement(By.Id("correo1")).SendKeys("joalva18@gmail.com");
            driver.FindElement(By.Id("Contraseña1")).Click();
            driver.FindElement(By.Id("Contraseña1")).SendKeys("Pepe1515");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.CssSelector(".navbar-toggler-icon")).Click();
            driver.FindElement(By.LinkText("Notificaciones")).Click();
            driver.FindElement(By.CssSelector("h2")).Click();
            Assert.AreEqual(driver.FindElement(By.CssSelector("h2")).Text, "Mis productos publicados"); //Titulo de Vista

        }

        /*
         * Historia ID: NEO-23
         * Driver: Jorge
         * Navigators: Maria y Yulian
         * Task: 23.5 Testing y Correcciones
         * Descripcion: Se realiza un UI test para los productos publicados 
         */

        [TestMethod]
        public void verPopUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44324/ProductosDelDia");
            driver.Manage().Window.Size = new System.Drawing.Size(1536, 867);
            driver.FindElement(By.LinkText("Iniciar Sesión")).Click();
            driver.FindElement(By.Id("correo1")).Click();
            driver.FindElement(By.Id("correo1")).SendKeys("joalva18@gmail.com");
            driver.FindElement(By.Id("Contraseña1")).Click();
            driver.FindElement(By.Id("Contraseña1")).SendKeys("Pepe1515");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Ver Productos")).Click();
            driver.FindElement(By.CssSelector("h2")).Click();
            Assert.AreEqual(driver.FindElement(By.CssSelector("h2")).Text, "Productos Publicados");
        }

        // Este iba a ingresar el producto con imagen
        // Pero hay que validar y ver el testing porque al parecer
        // cuando se carga una imagen localmente se cae
        [TestMethod]
        public void IngresarProductoSinImagen() 
        {

            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/IniciarSesion");

            var loginBox = driver.FindElement(By.Id("correo1"));
            loginBox.SendKeys("joalva18@gmail.com");

            var pwBox = driver.FindElement(By.Id("Contraseña1"));
            pwBox.SendKeys("Pepe1515");


            driver.FindElement(By.Name("submit")).Click();

       

            /// Pone la pantalla en full screen
            driver.FindElement(By.CssSelector(".navbar-toggler-icon")).Click();
            driver.FindElement(By.LinkText("Mis Productos")).Click();
            driver.FindElement(By.CssSelector(".form-group > .btn")).Click();
            driver.FindElement(By.Id("Producto_Nombre")).Click();
            driver.FindElement(By.Id("Producto_Nombre")).SendKeys("Carro de Juguete");
            driver.FindElement(By.Id("Producto_PrecioEstimado")).SendKeys("1500");
            driver.FindElement(By.CssSelector(".checkbox:nth-child(11) > label")).Click();
            driver.FindElement(By.Id("Producto_Condicion")).Click();
            {
                var dropdown = driver.FindElement(By.Id("Producto_Condicion"));
                dropdown.FindElement(By.XPath("//option[. = 'Semi-nuevo']")).Click();
           }
            //driver.FindElement(By.Id("customFileLangHTML")).Click();
            //driver.FindElement(By.Id("customFileLangHTML")).SendKeys("C:\\fakepath\\carro-vehiculo-de-juguete-plastico-rivale-amarillo.jpg");
            driver.FindElement(By.CssSelector(".btn:nth-child(2)")).Click();

            driver.Quit();
        }


        // AS 1-7-2020 José Chaves Hurtado, Dayana Marín Mayorga
        // Story ID: Reb 22
        //Tarea técnica: Proyecto de pruebas
        [TestMethod]
        public void PruebaCrearCategoria()
        {

            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/IniciarSesion");

            var loginBox = driver.FindElement(By.Id("correo1"));
            loginBox.SendKeys("isaac@gmail.com");

            var pwBox = driver.FindElement(By.Id("Contraseña1"));
            pwBox.SendKeys("Pepe1515");


            driver.FindElement(By.Name("submit")).Click();

            driver.Navigate().GoToUrl("https://localhost:44324/Categorias/Index");

            driver.FindElement(By.LinkText("Crear nueva categoría")).Click();

            var cajanombre = driver.FindElement(By.Id("Nombre"));
            cajanombre.SendKeys("Prueba");
            driver.FindElement(By.Name("Submit")).Click();
            driver.Quit();

        }


        [TestMethod]
        public void DashboardTest()
        {

            var driver = new ChromeDriver();

            driver.Url= "https://localhost:44324/Registrado/IniciarSesion";

            var loginBox = driver.FindElement(By.Id("correo1"));
            loginBox.SendKeys("isaac@gmail.com");

            var pwBox = driver.FindElement(By.Id("Contraseña1"));
            pwBox.SendKeys("Pepe1515");


            driver.FindElement(By.Name("submit")).Click();

            driver.Url= "https://localhost:44324/Dashboard";

            driver.Url = "https://localhost:44324/Dashboard/ProductosPorCategoria?Length=9";
            driver.Url = "https://localhost:44324/Dashboard/EdadesClientes?Length=9";
            driver.Url = "https://localhost:44324/Dashboard/CalificacionesUsuarios?Length=9";

            driver.Quit();

        }

        [TestMethod]
        public void PruebaReactivarTemporal()
        {
            //Iniciar el navegador
            PruebaIngresoChrome();
            driver.Navigate().GoToUrl("https://localhost:44324/Registrado/IniciarSesion");
            //Insertar valor del correo
            var correo = driver.FindElement(By.Id("correo1"));
            correo.SendKeys("malgnr7@gmail.com");
            //Insertar valor de contraseña
            var password = driver.FindElement(By.Id("Contraseña1"));
            password.SendKeys("Pepe1515");
            driver.FindElement(By.Name("checkbox5")).Click();
            //Click en el boton de iniciar sesion
            driver.FindElement(By.Name("submit")).Click();
            //Terminar driver
            TearDown();
        }

    }

}

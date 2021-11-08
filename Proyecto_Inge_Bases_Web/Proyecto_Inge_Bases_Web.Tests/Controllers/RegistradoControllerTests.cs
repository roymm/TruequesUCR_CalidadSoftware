using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Proyecto_Inge_Bases_Web;
using Proyecto_Inge_Bases_Web.Controllers;
using Proyecto_Inge_Bases_Web.Models;
using Proyecto_Integrador_Bases_Inge.Controllers;

namespace Proyecto_Inge_Bases_Web.Tests.Controllers
{
    [TestClass]
    public class RegistradoControllerTests

    {

        [TestMethod]
        public void TestIndexNotNull()
        {
            RegistradoController registrado = new RegistradoController();
            ViewResult result = registrado.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EnviarCorreoNotNull()
        {
            RegistradoController controller = new RegistradoController();

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

            ViewResult result = controller.EnviarCorreo("joalva18@gmail.com", "Correo prueba") as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CondicionesUsoNotNull()
        {
            RegistradoController registrado = new RegistradoController();
            ViewResult result = registrado.CondicionesUso() as ViewResult;
            Assert.IsNotNull(result);


        }

        [TestMethod]
        public void IniciarSesionNotNull()
        {

            RegistradoController controller = new RegistradoController();

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

            ViewResult result = controller.IniciarSesion() as ViewResult;
            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void RegistroExitosoNotNull()
        {
            RegistradoController registrado = new RegistradoController();
            ViewResult result = registrado.RegistroExitoso() as ViewResult;
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void PerfilCompletoNotNull()
        {
            RegistradoController registrado = new RegistradoController();
            ViewResult result = registrado.PerfilCompleto() as ViewResult;
            Assert.IsNotNull(result);
        }

        /*Inicio de AS 1/7/20 MarioVargas_B67454 & RicardoAlfaro_B70257
         Historia Usuario: OC - 2.6
             */

        /*Metodo de unidad de prueba para ReactivarCuentaNotNull*/
        [TestMethod]
        public void ReactivarCuentaNotNull()
        {
            RegistradoController registrado = new RegistradoController();
            ViewResult result = registrado.ReactivarCuenta() as ViewResult;
            Assert.IsNotNull(result);
        }

        /*Metodo de unidad de prueba para ReactivarCuentaView*/
        [TestMethod]
        public void ReactivarCuentaView()
        {
            RegistradoController registrado = new RegistradoController();
            ViewResult result = registrado.ReactivarCuenta() as ViewResult;
            Assert.AreEqual("ReactivarCuenta", result.ViewName);
        }


        /*Inicio de AS 1/7/20 MarioVargas_B67454 & RicardoAlfaro_B70257
         Historia Usuario: Funcionalidad Reboot en nuestro controlador
             */

        /*Metodo de unidad de prueba para Cambio_Contrasena_ExitosoNotNull*/
        [TestMethod]
        public void Cambio_Contrasena_ExitosoNotNull()
        {
            RegistradoController registrado = new RegistradoController();
            ViewResult result = registrado.Cambio_Contrasena_Exitoso() as ViewResult;
            Assert.IsNotNull(result);
        }


        /*Metodo de unidad de prueba para Cambio_Contrasena_ExitosoView*/
        [TestMethod]
        public void Cambio_Contrasena_ExitosoView()
        {
            RegistradoController registrado = new RegistradoController();
            ViewResult result = registrado.Cambio_Contrasena_Exitoso() as ViewResult;
            Assert.AreEqual("CambioFueExitoso", result.ViewName);
        }


        /*Test de integracion para registrar un nuevo usuario */ 
        [TestMethod]
        public void RegistrarUsuarioNuevoIntegracion()
        {
            //Se crea el form
            FormCollection form = new FormCollection();
            //Agrega el correo
            form.Add("email", "julyvargas@gmail.com");
            //Agrega el password
            form.Add("Contraseña", "Pepe1515");
            //Se marca la casilla de terminos y condiciones
            form.Add("checkbox", "checked");
            RegistradoController controller = new RegistradoController();
            //Result
            var result = (RedirectToRouteResult)controller.ValidarRegistro(form);
            result.RouteValues["action"].Equals("Registro");
            Assert.AreEqual("Registro", result.RouteValues["action"]);
        }

        [TestMethod]

       public void ValidarRegistroTest()
       {
            // Actividad supervisada 1/7/2020, Driver Daniel Sancho , Navigator: Joshua Ramirez, - RegistradoController, Historia de usuario : OC-1.2 , OC-1.3
            // Este test solo verifica si el correo asociado ya existe en la base y por lo tanto no debe registrarse, no verifica el caso donde el correo aún no está guardado en la base
            // y por lo tanto debe registrarse.
            FormCollection form = new FormCollection();
            form.Add("email", "joalva18@gmail.com");
            form.Add("Contraseña", "Pepe1515");
            RegistradoController controller = new RegistradoController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["UserName2"]).Returns("joalva18@gmail.com"); 
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("joalva18@gmail.com");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var result = (RedirectToRouteResult)controller.ValidarRegistro(form);  
            result.RouteValues["action"].Equals("Registro");
            Assert.AreEqual("Registro", result.RouteValues["action"]);
        }

      }
}

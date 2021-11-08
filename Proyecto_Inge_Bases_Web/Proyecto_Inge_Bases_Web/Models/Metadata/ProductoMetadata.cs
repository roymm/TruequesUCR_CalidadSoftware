using System;
using System.ComponentModel.DataAnnotations;



/**
         *  Función: Clase de Metadatos para validar las entradas. 
         * Autor : Neon
**/

namespace Proyecto_Inge_Bases_Web.Models
{
    public partial class ProductoMetadata
    {
        [Display(Name = "Nombre del Producto *")]
        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        public string Nombre;

        [RegularExpression(@"^([0-9]+(\.[0-9]+)?)", ErrorMessage = "*Introduce un número entre 0.1 y 100 000 000")]   /*Permite unicamente numeros. Con decimales o no. */
        [Range(0.1, 100000000, ErrorMessage = "*Introduce un número entre 0.1 y 100 000 000")] /*Establece el rango de los numeros que queremos*/
        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        [Display(Name = "Precio Estimado *")]
        public double PrecioEstimado;



        [Required(ErrorMessage = "*Este es un campo obligatorio.")]
        [Display(Name = "Condición *")]
        public string Condicion;


        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string Descripcion;

        [Display(Name = "Imagen 1")]
        public string ProductoImagen1;

        [Display(Name = "Imagen 2")]
        public string ProductoImagen2;

        [Display(Name = "Imagen 3")]
        public string ProductoImagen3;
    }
}
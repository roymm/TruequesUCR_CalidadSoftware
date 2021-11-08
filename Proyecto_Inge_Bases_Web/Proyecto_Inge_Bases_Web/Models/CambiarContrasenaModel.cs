using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Inge_Bases_Web.Models
{
    public class CambiarContrasenaModel
    {
       
        [Required(ErrorMessage = "Nueva Contraseña requerida", AllowEmptyStrings = false)]
        //[StringLength(12, ErrorMessage = "Debe tener entre 8 a 12 caracteres.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,12}$", ErrorMessage = "La nueva contraseña debe contener 1 letra mayúscula, 1 letra minúscula, 1 número, 8 caracteres mínimo y 12 máximo.")]
        [DataType(DataType.Password)]
        public string NuevaContrasena { get; set; }

        [DataType(DataType.Password)]
        [Compare("NuevaContrasena", ErrorMessage = "Debe ser igual a la nueva contraseña")]
        public string ConfirmarContrasena { get; set; }

        [Required]
        public string CodigoCambio { get; set; }
    }
}
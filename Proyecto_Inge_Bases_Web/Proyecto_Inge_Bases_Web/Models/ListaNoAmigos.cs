using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace Proyecto_Inge_Bases_Web.Models
{

    using System;
    using System.Collections.Generic;
    
    public partial class ListaNoAmigos
    {
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        [DbFunction("db", "FuncionNoAmigos")]
        public virtual IQueryable<funcionNoAmigos_Result> FuncionNoAmigos(string correo)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));

            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.CreateQuery<funcionNoAmigos_Result>("[db].[FuncionNoAmigos](@correo)", correoParameter);
        }
    }


}
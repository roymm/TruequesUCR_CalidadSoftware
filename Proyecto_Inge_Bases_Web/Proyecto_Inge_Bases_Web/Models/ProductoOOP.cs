using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Inge_Bases_Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

     [Table("ProductoOOP")]
    public abstract class ProductoOOP
    {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public ProductoOOP()
            {
                //this.Subastas = new HashSet<Subasta>();
                //this.Trueques = new HashSet<Trueque>();
                //this.Trueques1 = new HashSet<Trueque>();
                //this.Categorias = new HashSet<Categoria>();
            }

            public int ProductoID { get; set; }
            public string CorreoCliente { get; set; }
            public string Nombre { get; set; }
            public double PrecioEstimado { get; set; }
            public string Condicion { get; set; }
            public string Descripcion { get; set; }
            public bool Publicado { get; set; }
            public System.DateTime FechaRegistrado { get; set; }
            public Nullable<System.DateTime> FechaPublicado { get; set; }
            public Nullable<short> Calificacion { get; set; }
            public Nullable<bool> Estado { get; set; }
            public bool Seleccionado { get; set; }
            public byte[] ProductoImagen1 { get; set; }
            public byte[] ProductoImagen2 { get; set; }
            public byte[] ProductoImagen3 { get; set; }
            public virtual Cliente Cliente { get; set; }
        //public virtual ICollection<Subasta> Subastas { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Trueque> Trueques { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Trueque> Trueques1 { get; set; }
        //public virtual ICollection<Categoria> Categorias { get; set; }

        // metodo polimorfico

        public string Identificacion {


            get
            {

                return "Mi ID : " + ProductoID + " Mi Nombre : " + Nombre + " Mi Dueño : " + CorreoCliente;
            
            
            
            }
        
        
        }

        public virtual string FullIdentificacion 
        {


            get
            {

                return "Soy un Producto Normal " + Identificacion;
            
            }
        
        
        }

    }
}

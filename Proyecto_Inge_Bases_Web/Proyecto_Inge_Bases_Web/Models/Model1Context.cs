using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Proyecto_Inge_Bases_Web.Models
{

    /*  Esta clase es un contexto que permite usar la base
        pasandole por el lado a entetity framework.
     
        La conexion a TempPIEntities2 esta definida en el web config.
    
        Es igual que el connection string del modelo pero sin la especificacion de metadatafile de 
        Entity framework.
     
        Se empezo a trabajar en el Sprint 3 por motivos de refactoring.


     */
    public class Model1Context : DbContext
    {
        // Constructor que solo referencia la base de datos
        // Se esta manejando otra conection string
        // ya que se esta brincando el modelo y entity
        public Model1Context()
        : base("name=TempPIEntities2")
        {
        }

        /*
         Este metodo necesita ser usado ya que se esta usando un contexto o un uso derivado del modelo. 
         
         
         */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoOOP>().ToTable("ProductoOOP");
            modelBuilder.Entity<FisicoOOP>().ToTable("FisicoOOP");
            modelBuilder.Entity<VirtualOOP>().ToTable("VirtualOOP");
        }



    }
}
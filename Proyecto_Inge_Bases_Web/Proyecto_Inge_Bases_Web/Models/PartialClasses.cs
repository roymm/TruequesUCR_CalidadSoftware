using System.ComponentModel.DataAnnotations;

namespace Proyecto_Inge_Bases_Web.Models
{
    //Conexion para las validaciones de Producto y su modelo
    [MetadataType(typeof(ProductoMetadata))] 
    public partial class Producto { }

    [MetadataType(typeof(VirtualMetadata))]
    public partial class Virtual { }

    [MetadataType(typeof(SubastaMetadata))]
    public partial class Subasta { }

    //Conexion para las validaciones de Registrado y su modelo
    [MetadataType(typeof(RegistradoMetadata))]
    public partial class Registrado { }

    //Conexion para las validaciones de Cliente y su modelo
    [MetadataType(typeof(ClienteMetadata))]
    public partial class Cliente { }
}


 
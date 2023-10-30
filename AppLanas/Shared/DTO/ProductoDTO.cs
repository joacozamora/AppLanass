using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLanas.Shared.DTO
{
    public class ProductoDTO
    {
        [Required(ErrorMessage = "EL Nombre del producto debe ser OBLIGATORIO")]
        [MaxLength(40, ErrorMessage = "Solo se aceptan hasta 40 caracteres en el Nombre del Deposito")]
        public string nombreProducto { get; set; }

        //[Required(ErrorMessage = "EL Precio del Producto debe ser OBLIGATORIO")]
        //[MaxLength(40, ErrorMessage = "Solo se aceptan hasta 10 caracteres en el Precio del Producto")]
        //public decimal precioProducto { get; set; }

        [Required(ErrorMessage = "El Precio del Producto comprado a proveedores debe ser OBLIGATORIO")]
        public decimal precioProveedor { get; set; }

        [Required(ErrorMessage = "El Porcentaje del Producto que se desea obtener debe ser OBLIGATORIO")]
        public decimal porcentajeGanancia { get; set; }

        //conexion
        //public int ventaId { get; set; }

 

       
    }
}

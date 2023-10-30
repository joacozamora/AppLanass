using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLanas.BD.Data.Entity
{
	public class ProductoVenta
	{
		public int Id { get; set; }

		public int ProductoId { get; set; }

		public int VentaId {  get; set; }

		public Producto Producto { get; set; }

		public Venta Venta { get; set;}
	}
}

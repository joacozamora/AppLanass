using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLanas.BD.Data.Entity
{
    public class Caja
    {
        public int Id { get; set; }

        public DateTime fecha { get; set; }

        public List<Venta> Ventas { get; set; }
    }
}

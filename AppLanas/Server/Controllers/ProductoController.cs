using AppLanas.BD.Data;
using AppLanas.BD.Data.Entity;
using AppLanas.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppLanas.Server.Controllers
{
    [ApiController]
    [Route("api/Producto")]
    public class ProductoController : ControllerBase
    {
        private readonly Context context;

        public ProductoController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            var lista = await context.Productos.ToListAsync();
            if (lista is null)
            {
                return BadRequest("no hay Producto cargado");
            }

            return Ok(lista);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var existe = await context.Productos.AnyAsync(x => x.id == id);
            if (!existe)
            {
                return NotFound($"El producto {id} no existe");
            }

            return await context.Productos.FirstOrDefaultAsync(pro => pro.id == id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(ProductoDTO entidad)
        {
			var existe = await context.Productos.FirstOrDefaultAsync(x => x.nombreProducto == entidad.nombreProducto);
			if (existe != null)
			{
				return NotFound($"Este producto ya existe");
			}
			try
            {
				


				Producto nuevoproducto = new Producto();
   
                nuevoproducto.nombreProducto = entidad.nombreProducto;
                nuevoproducto.precioProducto = entidad.precioProveedor + (entidad.precioProveedor * entidad.porcentajeGanancia/100);
                nuevoproducto.precioProveedor = entidad.precioProveedor;
                nuevoproducto.porcentajeGanancia = entidad.porcentajeGanancia;

                await context.AddAsync(nuevoproducto);
                await context.SaveChangesAsync();
                return Ok("Se cargo correctamente el Producto.");
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(ProductoDTO productoDTO, int id)
        {
			try
			{
				var producto = await context.Productos.FirstOrDefaultAsync(e => e.id == id);
				if (producto != null)
				{
					producto.nombreProducto = productoDTO.nombreProducto;
					producto.precioProveedor = productoDTO.precioProveedor;
					producto.precioProducto = productoDTO.precioProveedor + (productoDTO.precioProveedor * productoDTO.porcentajeGanancia / 100);
					producto.porcentajeGanancia = productoDTO.porcentajeGanancia;
					await context.SaveChangesAsync();

				}
				else
				{
					return NotFound($"El producto de id={id} no existe");
				}
			}
			catch (Exception ex)
			{
				return NotFound($"Error al intentar editar el producto");

			}
			return Ok();
			//if (id != producto.id)
			//{
			//    BadRequest("El id de producto no coincide.");
			//}
			//var existe = await context.Productos.AnyAsync(x => x.id == id);
			//if (!existe)
			//{
			//    return NotFound($"El producto con el ID={id} no existe");
			//}

			//context.Update(producto);
			//await context.SaveChangesAsync();
			//return Ok();
		}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Productos.AnyAsync(x => x.id == id);
            if (!existe)
            {
                return BadRequest($"El producto con el ID={id} no existe");
            }

            context.Remove(new Producto() { id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

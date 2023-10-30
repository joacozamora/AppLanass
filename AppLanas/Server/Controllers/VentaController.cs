using AppLanas.BD.Data;
using AppLanas.BD.Data.Entity;
using AppLanas.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppLanas.Server.Controllers
{
    [ApiController]
    [Route("api/Venta")]
    public class VentaController : ControllerBase
    {
        private readonly Context context;

        public VentaController(Context context)
        {
            this.context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<Venta>>> Get()
        {
            var lista = await context.Ventas.Include(x=> x.ProductoVentas).ToListAsync();
            if (lista == null || lista.Count == 0)
            {
                return BadRequest("no hay Ventas cargada");
            }

            return Ok(lista);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Venta>> Get(int id)
        {
            var existe = await context.Ventas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"La Venta {id} no existe");
            }

            return await context.Ventas.FirstOrDefaultAsync(ven => ven.Id == id);
        }

        [HttpPost("{id:int}")]

        public async Task<ActionResult<int>> Post(VentaDTO entidad)
        {
            try
            {
                var existe = await context.Cajas.AnyAsync(x => x.Id == entidad.idCaja);
                if (!existe)
                {
                    return NotFound($"La caja de id = {entidad.idCaja} no existe");        
                }

                Venta nuevaventa = new Venta();

                nuevaventa.idCaja = entidad.idCaja;
                nuevaventa.totalGanancia = entidad.totalGanancia;

				await context.AddAsync(nuevaventa);
                await context.SaveChangesAsync();


                entidad.ListaProducto.ForEach(async x =>
                {
                    var newProductoVenta = new ProductoVenta
                    {
                        VentaId = nuevaventa.Id,
                        ProductoId = x.Id   
                    };

                    await context.AddAsync(newProductoVenta);
                    await context.SaveChangesAsync();
                });


				return Ok("Se cargo la venta correctamente");

			}

			catch (Exception e)
            {
                return BadRequest(e.Message);
            }
   
          
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Venta venta, int id)
        {
            if (id != venta.Id)
            {
                BadRequest("El id de venta no coincide.");
            }
            var existe = await context.Ventas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"La venta con el ID={id} no existe");
            }

            context.Update(venta);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Ventas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return BadRequest($"La venta con el ID={id} no existe");
            }

            context.Remove(new Venta() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }



    }
}

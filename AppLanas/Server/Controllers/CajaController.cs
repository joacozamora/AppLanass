using AppLanas.BD.Data;
using AppLanas.BD.Data.Entity;
using AppLanas.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppLanas.Server.Controllers
{
    [ApiController]
    [Route("api/Caja")]
    public class CajaController : ControllerBase
    {
        private readonly Context context;

        public CajaController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Caja>>> Get()
        {
            var lista = await context.Cajas.ToListAsync();
            if (lista == null || lista.Count == 0)
            {
                return BadRequest("no hay caja cargada");
            }

            return Ok(lista);

        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Caja>> Get(int id)
        {
            var existe = await context.Cajas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"la caja {id} no existe");
            }
            return await context.Cajas.FirstOrDefaultAsync(caj => caj.Id == id);
        }

        [HttpPost]

        public async Task<ActionResult<int>> Post(CajaDTO entidad)
        {
           

            try
            {
                var existe = await context.Cajas.AnyAsync(x => x.Id == entidad.Id);
                if (!existe)
                {
                    return NotFound($"La venta de id = {entidad.Id} no existe");
                }

                Caja nuevacaja = new Caja();
                nuevacaja.Id = entidad.Id;
                nuevacaja.fecha = entidad.fecha;


                await context.AddAsync(nuevacaja);
                await context.SaveChangesAsync();
                return Ok("Se cargo correctamente la caja");
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Caja caja, int id)
        {
            if (id != caja.Id)
            {
                BadRequest("El id del componente no coincide.");
            }
            var existe = await context.Cajas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El componente con el ID={id} no existe");
            }

            context.Update(caja);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Cajas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return BadRequest($"La caja con el ID={id} no existe");
            }

            context.Remove(new Producto() { id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}


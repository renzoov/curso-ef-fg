using EFCorePeliculas.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace EFCorePeliculas.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GenerosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Genero>> Get()
        {
            //return await context.Generos.Where(x => !x.EstaBorrado).OrderBy(g => g.Nombre).ToListAsync();
            context.Logs.Add(new Log() { Id = Guid.NewGuid(), Mensaje = "Ejecutando el método GenerosController.Get" });
            await context.SaveChangesAsync();
            return await context.Generos.OrderByDescending(g => EF.Property<DateTime>(g, "FechaCreacion")).ToListAsync();
        }

        [HttpGet("procedimiento_almacenado/{id:int}")]
        public async Task<ActionResult<Genero>> GetSP(int id)
        {
            var generos = context.Generos
                .FromSqlInterpolated($"EXEC Generos_ObtenerPorId {id}")
                .IgnoreQueryFilters()
                .AsAsyncEnumerable();

            await foreach(var genero in generos)
            {
                return genero;
            }

            return NotFound();
        }

        [HttpPost("procedimiento_almacenado")]
        public async Task<ActionResult> PostSP(Genero genero)
        {
            var existeGeneroConNombre = await context.Generos.AnyAsync(g => g.Nombre == genero.Nombre);

            if (existeGeneroConNombre) return BadRequest("Ya existe un género con ese nombre: " + genero.Nombre);

            var outputId = new SqlParameter();
            outputId.ParameterName = "@id";
            outputId.SqlDbType = System.Data.SqlDbType.Int;
            outputId.Direction = System.Data.ParameterDirection.Output;

            await context.Database
                .ExecuteSqlRawAsync("EXEC Generos_Insertar @nombre = {0}, @id = {1} OUTPUT", genero.Nombre, outputId);

            var id = (int)outputId.Value;
            return Ok(id);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            //var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);

            //var genero = await context.Generos
            //    .FromSqlRaw("SELECT * FROM Generos WHERE Identificador = {0}", id)
            //    .IgnoreQueryFilters()
            //    .FirstOrDefaultAsync();

            var genero = await context.Generos
                .FromSqlInterpolated($"SELECT * FROM Generos WHERE Identificador = {id}")
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();

            if (genero is null) return NotFound();

            var fechaCreacion = context.Entry(genero).Property<DateTime>("FechaCreacion").CurrentValue;

            return Ok(new
            {
                Id = genero.Identificador,
                Nombre = genero.Nombre,
                fechaCreacion
            });
        }

        [HttpGet("primer")]
        public async Task<ActionResult<Genero>> Primer()
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Nombre.StartsWith("c"));

            if (genero is null) return NotFound();

            return genero;
        }

        [HttpGet("filtrar")]
        public async Task<IEnumerable<Genero>> Filtrar(string nombre)
        {
            return await context.Generos.Where(g => g.Nombre.Contains(nombre)).ToListAsync();
        }

        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetPaginacion(int pagina = 1)
        {
            var cantidadRegistreosPorPagina = 2;
            var generos = await context.Generos
                .Skip((pagina - 1) * cantidadRegistreosPorPagina)
                .Take(cantidadRegistreosPorPagina)
                .ToListAsync();
            return generos;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Genero genero)
        {
            //var estatus1 = context.Entry(genero).State;
            //context.Add(genero);
            //var estatus2 = context.Entry(genero).State;
            //await context.SaveChangesAsync();
            //var estatus3 = context.Entry(genero).State;

            var existeGeneroConNombre = await context.Generos.AnyAsync(g => g.Nombre == genero.Nombre);

            if(existeGeneroConNombre) return BadRequest($"Ya existe un género con el nombre {genero.Nombre}.");

            //context.Add(genero);
            //context.Entry(genero).State = EntityState.Added;
            await context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO Generos(Nombre)
                VALUES({genero.Nombre}");
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(Genero[] generos)
        {
            context.AddRange(generos);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("agregar2")]
        public async Task<ActionResult> Agregar2(int id)
        {
            var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);

            if(genero is null) return NotFound();

            genero.Nombre += " 2";
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Genero genero)
        {
            context.Update(genero);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null) return NotFound();

            context.Remove(genero);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("borradoSuave/{id:int}")]
        public async Task<ActionResult> DeleteSuave(int id)
        {
            var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null) return NotFound();

            genero.EstaBorrado = true;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("restaurar/{id:int}")]
        public async Task<ActionResult> Restaurar(int id)
        {
            var genero = await context.Generos.AsTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null) return NotFound();

            genero.EstaBorrado = false;
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

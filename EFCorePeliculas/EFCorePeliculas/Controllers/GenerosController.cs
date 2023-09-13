﻿using EFCorePeliculas.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return await context.Generos.OrderBy(g => g.Nombre).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null) return NotFound();

            return genero;
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
            var estatus1 = context.Entry(genero).State;
            context.Add(genero);
            var estatus2 = context.Entry(genero).State;
            await context.SaveChangesAsync();
            var estatus3 = context.Entry(genero).State;
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
    }
}

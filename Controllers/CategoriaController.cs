using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar categorias.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly OrderContext _context;

        public CategoriaController(OrderContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todas as categorias.
        /// </summary>
        /// <returns>Uma lista de categorias.</returns>
        /// <response code="200">Retorna as categorias com sucesso</response>
        /// <response code="500">Erro interno no servidor</response>
        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        /// <summary>
        /// Retorna uma categoria específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da categoria.</param>
        /// <returns>Uma categoria específica.</returns>
        /// <response code="200">Categoria encontrada com sucesso</response>
        /// <response code="404">Categoria não encontrada</response>
        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="id">O ID da categoria.</param>
        /// <param name="categoria">Os novos dados da categoria.</param>
        /// <returns>Status de sucesso ou erro.</returns>
        /// <response code="204">Categoria atualizada com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Categoria não encontrada</response>
        // PUT: api/Categoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoria">Os dados da nova categoria.</param>
        /// <returns>A categoria criada com sucesso.</returns>
        /// <response code="201">Categoria criada com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        // POST: api/Categoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.IdCategoria }, categoria);
        }

        /// <summary>
        /// Exclui uma categoria pelo ID.
        /// </summary>
        /// <param name="id">O ID da categoria.</param>
        /// <returns>Status de sucesso ou erro.</returns>
        /// <response code="204">Categoria excluída com sucesso</response>
        /// <response code="404">Categoria não encontrada</response>
        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.IdCategoria == id);
        }
    }
}

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
    /// Controlador para gerenciar produtos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly OrderContext _context;

        public ProdutoController(OrderContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        /// <response code="200">Retorna os produtos com sucesso</response>
        /// <response code="500">Erro interno no servidor</response>
        // GET: api/Produto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

         /// <summary>
        /// Retorna um produto específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>Um produto específico.</returns>
        /// <response code="200">Produto encontrado com sucesso</response>
        /// <response code="404">Produto não encontrado</response>
        // GET: api/Produto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <param name="produto">Os novos dados do produto.</param>
        /// <returns>Status de sucesso ou erro.</returns>
        /// <response code="204">Produto atualizado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Produto não encontrado</response>
        // PUT: api/Produto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.IdProduto)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produto">Os dados do novo produto.</param>
        /// <returns>O produto criado com sucesso.</returns>
        /// <response code="201">Produto criado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        // POST: api/Produto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.IdProduto }, produto);
        }

        /// <summary>
        /// Exclui um produto pelo ID.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>Status de sucesso ou erro.</returns>
        /// <response code="204">Produto excluído com sucesso</response>
        /// <response code="404">Produto não encontrado</response>
        // DELETE: api/Produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.IdProduto == id);
        }
    }
}

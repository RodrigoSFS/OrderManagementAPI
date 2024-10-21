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
    /// Controlador para gerenciar clientes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly OrderContext _context;

        public ClienteController(OrderContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todos os clientes.
        /// </summary>
        /// <returns>Uma lista de clientes.</returns>
        /// <response code="200">Retorna os clientes com sucesso</response>
        /// <response code="500">Erro interno no servidor</response>
        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        /// <summary>
        /// Retorna um cliente específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do cliente.</param>
        /// <returns>Um cliente específico.</returns>
        /// <response code="200">Cliente encontrado com sucesso</response>
        /// <response code="404">Cliente não encontrado</response>
        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente.</param>
        /// <param name="cliente">Os novos dados do cliente.</param>
        /// <returns>Status de sucesso ou erro.</returns>
        /// <response code="204">Cliente atualizado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Cliente não encontrado</response>
        // PUT: api/Cliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="cliente">Os dados do novo cliente.</param>
        /// <returns>O cliente criado com sucesso.</returns>
        /// <response code="201">Cliente criado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        // POST: api/Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.IdCliente }, cliente);
        }

        /// <summary>
        /// Exclui um cliente pelo ID.
        /// </summary>
        /// <param name="id">O ID do cliente.</param>
        /// <returns>Status de sucesso ou erro.</returns>
        /// <response code="204">Cliente excluído com sucesso</response>
        /// <response code="404">Cliente não encontrado</response>
        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}

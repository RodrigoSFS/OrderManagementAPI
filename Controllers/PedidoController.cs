using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.Models;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar pedidos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;
        private readonly OrderContext _context;

        public PedidoController(PedidoService pedidoService, OrderContext context)
        {
            _pedidoService = pedidoService;
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todos os pedidos.
        /// </summary>
        /// <returns>Uma lista de pedidos.</returns>
        /// <response code="200">Retorna os pedidos com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        // GET: api/Pedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        /// <summary>
        /// Retorna um pedido específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do pedido.</param>
        /// <returns>Um pedido específico.</returns>
        /// <response code="200">Pedido encontrado com sucesso.</response>
        /// <response code="404">Pedido não encontrado.</response>
        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _pedidoService.GetPedidoByIdAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        /// <summary>
        /// Retorna pedidos com um status específico.
        /// </summary>
        /// <param name="status">O status do pedido.</param>
        /// <returns>Uma lista de pedidos com o status informado.</returns>
        /// <response code="200">Pedidos encontrados com sucesso.</response>
        /// <response code="404">Nenhum pedido encontrado com o status informado.</response>
        // GET: api/Pedido/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosByStatus(PedidoStatus status)
        {
            var pedidos = await _pedidoService.GetPedidosByStatusAsync(status);

            if (!pedidos.Any())
            {
                return NotFound();
            }

            return Ok(pedidos);
        }

        /// <summary>
        /// Atualiza um pedido existente.
        /// </summary>
        /// <param name="id">O ID do pedido a ser atualizado.</param>
        /// <param name="pedidoDto">Dados atualizados do pedido.</param>
        /// <response code="204">Pedido atualizado com sucesso.</response>
        /// <response code="400">Requisição inválida.</response>
        /// <response code="404">Pedido não encontrado.</response>
        // PUT: api/Pedido/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, PedidoDto pedidoDto)
        {
            try
            {
                var result = await _pedidoService.UpdatePedidoAsync(id, pedidoDto);
                if (!result) return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza o status de um pedido.
        /// </summary>
        /// <param name="id">O ID do pedido a ser atualizado.</param>
        /// <param name="newStatus">O novo status do pedido.</param>
        /// <response code="204">Status do pedido atualizado com sucesso.</response>
        /// <response code="400">Requisição inválida.</response>
        /// <response code="404">Pedido não encontrado.</response>
        // PUT: api/Pedido/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePedidoStatus(int id, PedidoStatus newStatus)
        {
            try
            {
                var result = await _pedidoService.UpdatePedidoStatusAsync(id, newStatus);
                if (!result) return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="pedidoDto">Dados do novo pedido.</param>
        /// <returns>O pedido criado.</returns>
        /// <response code="201">Pedido criado com sucesso.</response>
        /// <response code="400">Requisição inválida.</response>
        // POST: api/Pedido
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(PedidoDto pedidoDto)
        {
            var pedido = await _pedidoService.CreatePedidoAsync(pedidoDto);
            return CreatedAtAction("GetPedido", new { id = pedido.IdPedido }, pedido);
        }

        /// <summary>
        /// Remove um pedido específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do pedido a ser removido.</param>
        /// <response code="204">Pedido removido com sucesso.</response>
        /// <response code="404">Pedido não encontrado.</response>
        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var result = await _pedidoService.DeletePedidoAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedido == id);
        }
    }
}

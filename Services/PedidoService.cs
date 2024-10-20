using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Services
{
    public class PedidoService
    {
        private readonly OrderContext _context;

        public PedidoService(OrderContext context)
        {
            _context = context;
        }

        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            return await _context.Pedidos
                                .Include(p => p.PedidoProdutos)
                                .ThenInclude(pp => pp.Produto)
                                .FirstOrDefaultAsync(p => p.IdPedido == id);
        }

        public async Task<Pedido> CreatePedidoAsync(PedidoDto pedidoDto)
        {
            var pedido = new Pedido
            {
                IdCliente = pedidoDto.IdCliente,
                Status = PedidoStatus.Aberto
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            // Add PedidoProdutos
            foreach (var item in pedidoDto.PedidoProdutos)
            {
                var pedidoProduto = new PedidoProduto
                {
                    IdPedido = pedido.IdPedido,
                    IdProduto = item.IdProduto,
                    Quantidade = item.Quantidade
                };
                _context.PedidoProdutos.Add(pedidoProduto);
            }

            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<bool> UpdatePedidoAsync(int id, PedidoDto pedidoDto)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null || pedido.Status == PedidoStatus.Fechado || pedido.Status == PedidoStatus.Cancelado)
            {
                // Cannot modify a "Fechado" or "Cancelado" order
                throw new InvalidOperationException("Cannot modify a closed or canceled order.");
            }

            // Update Pedido logic (e.g., updating products or quantities)
            pedido.PedidoProdutos.Clear();
            foreach (var item in pedidoDto.PedidoProdutos)
            {
                pedido.PedidoProdutos.Add(new PedidoProduto
                {
                    IdProduto = item.IdProduto,
                    Quantidade = item.Quantidade
                });
            }

            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdatePedidoStatusAsync(int id, PedidoStatus newStatus)
        {
            var pedido = await GetPedidoByIdAsync(id);
            if (pedido == null) return false;

            if (newStatus == PedidoStatus.Fechado && !pedido.PedidoProdutos.Any())
            {
                throw new InvalidOperationException("Pedido cannot be closed without products.");
            }

            if (pedido.Status == PedidoStatus.Fechado || pedido.Status == PedidoStatus.Cancelado)
            {
                throw new InvalidOperationException("Pedido cannot be modified when it's closed or canceled.");
            }

            pedido.Status = newStatus;
            pedido.Data = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null || pedido.Status == PedidoStatus.Fechado || pedido.Status == PedidoStatus.Cancelado)
            {
                // Cannot delete a "Fechado" or "Cancelado" order
                return false;
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementAPI.Models
{

    public enum PedidoStatus
    {
        Aberto,
        Fechado,
        Cancelado
    }
    
    public class Pedido
    {   
        [Key]
        public int IdPedido { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        public PedidoStatus Status { get; set; } = PedidoStatus.Aberto;

        // Navigation property
        public ICollection<PedidoProduto> PedidoProdutos { get; set; }
    }

    public class PedidoDto
    {
        public DateTime Data { get; set; }
        public int IdCliente { get; set; }
        public List<PedidoProdutoDto> PedidoProdutos { get; set; }
    }
}
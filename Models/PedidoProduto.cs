using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementAPI.Models
{
    public class PedidoProduto
    {
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }

        public int IdProduto { get; set; }
        public Produto Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        // Total price for this product in the order
        [NotMapped]
        public decimal TotalPrice => Produto.Preco * Quantidade;
    }
}
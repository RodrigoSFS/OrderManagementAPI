using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementAPI.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]

        // Navigation property
        public ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();
    }
}
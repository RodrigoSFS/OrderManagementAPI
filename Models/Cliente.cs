using System.ComponentModel.DataAnnotations;

namespace OrderManagementAPI.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Endereco { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefone { get; set; }

        [Required]
        public int LimiteDeCredito { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}
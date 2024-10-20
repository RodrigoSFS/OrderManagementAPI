
using System.ComponentModel.DataAnnotations;

namespace OrderManagementAPI.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set;}

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
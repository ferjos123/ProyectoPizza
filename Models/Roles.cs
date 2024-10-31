using System.ComponentModel.DataAnnotations;

namespace PizzeriaWeb3._1.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<Usuarios>? Usuarios { get; set; }
    }
}

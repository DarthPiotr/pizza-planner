using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pizza_planner.Models
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public override string ToString()
        {
            return Name;
        }
    }
}
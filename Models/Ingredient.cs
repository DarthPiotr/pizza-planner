using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pizza_planner.Models
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PizzaIngredients> PizzaIngredients { get; set; } = new List<PizzaIngredients>();

        public ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }
}
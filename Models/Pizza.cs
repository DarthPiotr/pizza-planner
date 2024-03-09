using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza_planner.Models
{
    public class Pizza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public PizzaSize Size { get; set; }
        public PizzaCrust Crust { get; set; }

        public ICollection<PizzaIngredients> PizzaIngredients { get; set; } = new List<PizzaIngredients>();

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}

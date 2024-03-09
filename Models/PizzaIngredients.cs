using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza_planner.Models
{
    [PrimaryKey(nameof(PizzaId), nameof(IngredientId))]
    public class PizzaIngredients
    {
        public int PizzaId { get; set; }
        public int IngredientId { get; set; }

        public Pizza? Pizza { get; set; }
        public Ingredient? Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}

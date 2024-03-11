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
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal Price { get; set; }
        [Required]
        public PizzaSize Size { get; set; }
        [Required]
        public PizzaCrust Crust { get; set; }

        public ICollection<Ingredient>? Ingredients { get; set; } = new List<Ingredient>();
    }
}

using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace pizza_planner.Models
{
    public class DataContext : DbContext
    {
        private IConfiguration _configuration;

        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<PizzaIngredients> PizzaIngredients { get; set; }

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("Sqlite") ?? "";
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}

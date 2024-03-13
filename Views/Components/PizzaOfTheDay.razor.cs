using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using pizza_planner.Models;
using System.Collections.Generic;

namespace pizza_planner.Views.Components
{
    public partial class PizzaOfTheDay
    {
        [Inject]
        private DataContext? Context { get; set; }
        private readonly Random random = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            RefreshPizza();
        }

        public void RefreshPizza()
        {
            if (Context != null)
            {
                List<Pizza> pizzas = Context.Pizzas.Include(p => p.Ingredients).ToList();
                int randomIndex = random.Next(0, pizzas.Count);
                SelectedPizza = pizzas[randomIndex];
            }
        }
    }
}

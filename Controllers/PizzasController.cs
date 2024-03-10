using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using pizza_planner.Models;

namespace pizza_planner.Controllers
{
    public class PizzasController : Controller
    {
        private readonly DataContext _context;

        public PizzasController(DataContext context)
        {
            _context = context;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index(string? query)
        {
            ViewBag.Query = query;

            IQueryable<Pizza> pizzaQuery = _context.Pizzas
                .Include(p => p.Ingredients);

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                pizzaQuery = pizzaQuery.Where(p =>
                        p.Name.ToLower().Contains(query) 
                    );
            }

            return View(await pizzaQuery.ToListAsync());
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .Include (p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            ViewBag.Ingredients = _context.Ingredients.ToList();

            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                var pizza = new Pizza();
                this.FillPizzaInfo(pizza, collection);

                _context.Add(pizza);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Create();
            }
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
                var pizza = await _context.Pizzas
                .Include( p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Ingredients = _context.Ingredients.ToList();

            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {

            try
            {
                var pizza = await _context.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == id);

                if (pizza == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                this.FillPizzaInfo(pizza, collection);

                _context.Update(pizza);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            } 
            catch
            {
                return await Edit(id);
            }
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza != null)
            {
                _context.Pizzas.Remove(pizza);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }

        private void FillPizzaInfo(Pizza pizza, IFormCollection collection)
        {
            pizza.Name = collection["Name"].ToString();
            pizza.Price = Convert.ToDouble(collection["Price"].ToString());
            pizza.Size = (PizzaSize)Convert.ToInt32(collection["Size"].ToString());
            pizza.Crust = (PizzaCrust)Convert.ToInt32(collection["Crust"].ToString());
            pizza.Ingredients?.Clear();

            if (collection.ContainsKey("Ingredients"))
            {
                foreach (var ingredientString in collection["Ingredients"])
                {
                    if (!int.TryParse(ingredientString, out int ingredientId))
                    {
                        continue;
                    }

                    Ingredient? ingredient = _context.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
                    if (ingredient != null)
                    {
                        pizza.Ingredients?.Add(ingredient);
                    }
                }
            }
        }
    }
}

using ContosoPizza.Models;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;

    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pizza>> GetAll() 
        => await _context.Pizzas
            .AsNoTracking()
            .ToListAsync();

    public async Task<Pizza?> GetById(int id)
        => await _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);

    public async Task<Pizza> Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        await _context.SaveChangesAsync();

        return newPizza;
    }

    public async Task AddTopping(int pizzaId, int toppingId)
    {
        var pizzaToUpdate = await _context.Pizzas.FindAsync(pizzaId);
        var toppingToAdd = await _context.Toppings.FindAsync(toppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or topping does not exist");
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateSauce(int pizzaId, int sauceId)
    {
        var pizzaToUpdate = await _context.Pizzas.FindAsync(pizzaId);
        var sauceToUpdate = await _context.Sauces.FindAsync(sauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new InvalidOperationException("Pizza or sauce does not exist");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var pizzaToDelete = await _context.Pizzas.FindAsync(id);
        if (pizzaToDelete is not null)
        {
            _context.Pizzas.Remove(pizzaToDelete);
            await _context.SaveChangesAsync();
        }        
    }
}

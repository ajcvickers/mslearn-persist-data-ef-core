using Microsoft.EntityFrameworkCore;
using ContosoPizza.Models;

namespace ContosoPizza.Data;

public class PizzaContext : DbContext
{
    public PizzaContext (DbContextOptions<PizzaContext> options)
        : base(options)
    {
    }

    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Topping> Toppings => Set<Topping>();
    public DbSet<Sauce> Sauces => Set<Sauce>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>(b =>
        {
            b.Property(e => e.Id);
            b.Property(e => e.Name);
        });
        modelBuilder.Entity<Topping>(b =>
        {
            b.Property(e => e.Id);
            b.Property(e => e.Name);
            b.Property(e => e.Calories);
        });
        modelBuilder.Entity<Sauce>(b =>
        {
            b.Property(e => e.Id);
            b.Property(e => e.Name);
            b.Property(e => e.IsVegan);
        });
    }
}

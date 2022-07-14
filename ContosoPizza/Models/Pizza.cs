using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoPizza.Models;

public class Pizza : IHazLogger, IHazKey
{
    public Pizza(string name)
    {
        Name = name;
    }

    public Pizza(string name, Sauce? sauce)
    {
        Name = name;
        Sauce = sauce;
    }

    public int Id { get; }
    public string Name { get; }
    
    public Sauce? Sauce { get; private set; }

    public void UpdateSauce(Sauce? newSauce)
    {
        Logger?.LogInformation(1, $"Updating sauce for '{Name}' from '{Sauce?.Name}' to '{newSauce?.Name}'.");
        
        Sauce = newSauce;
    }

    public ICollection<Topping> Toppings { get; } = new List<Topping>();
    
    [NotMapped]
    public ILogger? Logger { get; set; }
}

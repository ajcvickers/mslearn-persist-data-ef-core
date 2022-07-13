using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models;

public class Pizza
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
    
    public Sauce? Sauce { get; set; }

    public ICollection<Topping> Toppings { get; } = new List<Topping>();
}

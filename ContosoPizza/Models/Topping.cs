using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContosoPizza.Models;

public class Topping
{
    public Topping(string name, decimal calories)
    {
        Name = name;
        Calories = calories;
    }

    public int Id { get; }
    public string Name { get; }
    public decimal Calories { get; }

    [JsonIgnore] public ICollection<Pizza> Pizzas { get; } = new List<Pizza>();
}

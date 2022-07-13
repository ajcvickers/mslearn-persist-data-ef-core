using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ContosoPizza.Models;

public class Topping : IHazLogger, IHazKey
{
    public Topping(string name, decimal calories)
    {
        Name = name;
        Calories = calories;
    }

    public int Id { get; }
    public string Name { get; }
    public decimal Calories { get; }

    [NotMapped]
    public ILogger? Logger { get; set; }
}

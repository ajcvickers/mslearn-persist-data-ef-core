using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models;

public class Sauce
{
    public Sauce(string name, bool isVegan)
    {
        Name = name;
        IsVegan = isVegan;
    }

    public int Id { get; }
    public string Name { get; }
    public bool IsVegan { get; }
}

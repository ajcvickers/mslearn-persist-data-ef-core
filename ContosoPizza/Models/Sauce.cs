using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoPizza.Models;

public class Sauce : IHazLogger, IHazKey
{
    public Sauce(string name, bool isVegan)
    {
        Name = name;
        IsVegan = isVegan;
    }

    public int Id { get; }
    public string Name { get; }
    public bool IsVegan { get; }
    
    [NotMapped]
    public ILogger? Logger { get; set; }
}

namespace ContosoPizza.Models;

public interface IHazLogger
{
    ILogger? Logger { get; set; }
}
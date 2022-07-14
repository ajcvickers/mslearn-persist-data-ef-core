using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class ToppingsController : ControllerBase
{
    private readonly PizzaService _service;
    
    public ToppingsController(PizzaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Topping>> GetAll() 
        => await _service.GetAllToppings();
}
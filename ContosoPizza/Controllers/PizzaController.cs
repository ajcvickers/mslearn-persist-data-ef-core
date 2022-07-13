using ContosoPizza.Services;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaService _service;
    
    public PizzaController(PizzaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Pizza>> GetAll() 
        => await _service.GetAll();

    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> GetById(int id)
    {
        var pizza = await _service.GetById(id);

        return pizza is not null 
            ? pizza 
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pizza newPizza)
    {
        var pizza = await _service.Create(newPizza);
        return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}/addtopping")]
    public async Task<IActionResult> AddTopping(int id, int toppingId)
    {
        var pizzaToUpdate = await _service.GetById(id);

        if(pizzaToUpdate is not null)
        {
            await _service.AddTopping(id, toppingId);
            return NoContent();    
        }

        return NotFound();
    }

    [HttpPut("{id}/updatesauce")]
    public async Task<IActionResult> UpdateSauce(int id, int sauceId)
    {
        var pizzaToUpdate = await _service.GetById(id);

        if(pizzaToUpdate is not null)
        {
            await _service.UpdateSauce(id, sauceId);
            return NoContent();    
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pizza = await _service.GetById(id);

        if(pizza is not null)
        {
            await _service.DeleteById(id);
            return Ok();
        }

        return NotFound();
    }
}

using Microsoft.AspNetCore.Mvc;
using ShipAccounting.Data;
using ShipAccounting.Models;
using ShipAccounting.Models.ModelsSources;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipAccounting.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShipClassesController(DataDbContext dbContext) : ControllerBase
{
    // GET: api/<ShipClassesController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipClass>>> GetAllAsync() =>
        Ok(await Task.FromResult(dbContext.Classes));

    // GET api/<ShipClassesController>/id
    [HttpGet("{id}")]
    public async Task<ActionResult<ShipClass>> GetAsync(int id)
    {
        var shipClass = await dbContext.Classes.FindAsync(id);

        if (shipClass is null)
            return NotFound($"Ship class with id={id} not found!");

        return Ok(shipClass);
    }

    // POST api/<ShipClassesController>
    [HttpPost]
    public async Task<ActionResult<ShipClass>> PostAsync(ShipClass shipClass)
    {
        if (shipClass == null)
            return BadRequest("Ship class is null!");

        if (await dbContext.Classes.FindAsync(shipClass.Id) is not null)
            return Problem($"Ship class with id={shipClass.Id} already exists!");

        var newShipClass = await new Factory<ShipClass>()
                           .GetModel(dbContext, dbContext.Classes, shipClass);

        await dbContext.Classes.AddAsync(newShipClass);
        await dbContext.SaveChangesAsync();

        return Ok(newShipClass);
    }

    // PUT api/<ShipClassesController>
    [HttpPut]
    public async Task<ActionResult<ShipClass>> PutAsync(ShipClass shipClass)
    {
        var findShipClass = await dbContext.Classes.FindAsync(shipClass.Id);

        if (findShipClass is null)
            return NotFound($"Ship class with id={shipClass.Id} not found!");

        var updatedShipClass = await new Factory<ShipClass>()
                               .UpdateModel(dbContext, dbContext.Classes, shipClass);
        await dbContext.SaveChangesAsync();

        return Ok(updatedShipClass);
    }

    // DELETE api/<ShipClassesController>/id
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var shipClass = await dbContext.Classes.FindAsync(id);

        if (shipClass is null)
            return NotFound($"Ship class with id={id} not found!");

        dbContext.Classes.Remove(shipClass);
        await dbContext.SaveChangesAsync();

        return Ok("Remove Completed!");
    }
}

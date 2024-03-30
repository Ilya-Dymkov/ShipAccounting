using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipAccounting.Data;
using ShipAccounting.Models;
using ShipAccounting.Models.ModelsSources;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipAccounting.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShipsController(DataDbContext dbContext) : ControllerBase
{
    // GET: api/<ShipsController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ship>>> GetAllAsync() =>
        Ok(await Task.FromResult(dbContext.Ships
                                          .Include(s => s.Class)
                                          .Include(s => s.Outcomes)
                                          .ThenInclude(o => o.Battle)));

    // GET api/<ShipsController>/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Ship>> GetAsync(int id)
    {
        var ship = await dbContext.Ships
                                  .Include(s => s.Class)
                                  .Include(s => s.Outcomes)
                                  .ThenInclude(o => o.Battle)
                                  .FirstOrDefaultAsync(s => s.Id == id);

        if (ship is null)
            return NotFound($"Ship with id={id} not found!");

        return Ok(ship);
    }

    // POST api/<ShipsController>
    [HttpPost]
    public async Task<ActionResult<Ship>> PostAsync(Ship ship)
    {
        if (ship == null)
            return BadRequest("Ship is null!");

        if (await dbContext.Ships.FindAsync(ship.Id) is not null)
            return Problem($"Ship with id={ship.Id} already exists!");

        var newShip = await new Factory<Ship>()
                      .GetModel(dbContext, dbContext.Ships, ship);

        await dbContext.Ships.AddAsync(newShip);
        await dbContext.SaveChangesAsync();

        return Ok(newShip);
    }

    // PUT api/<ShipsController>
    [HttpPut]
    public async Task<ActionResult<Ship>> PutAsync(Ship ship)
    {
        var findShip = await dbContext.Ships.FindAsync(ship.Id);

        if (findShip is null)
            return NotFound($"Ship with id={ship.Id} not found!");

        var updatedShip = await new Factory<Ship>()
                          .UpdateModel(dbContext, dbContext.Ships, ship);
        await dbContext.SaveChangesAsync();

        return Ok(updatedShip);
    }

    // DELETE api/<ShipsController>/id
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var ship = await dbContext.Ships.FindAsync(id);

        if (ship is null)
            return NotFound($"Ship with id={id} not found!");

        if (ship.Outcomes is not null)
            dbContext.Outcomes.RemoveRange(ship.Outcomes);

        dbContext.Ships.Remove(ship);
        await dbContext.SaveChangesAsync();

        return Ok("Remove Completed!");
    }
}

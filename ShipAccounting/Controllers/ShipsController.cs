using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipAccounting.Data;
using ShipAccounting.Models;
using ShipAccounting.Models.CreatingModel;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipAccounting.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShipsController(DataDbContext context) : ControllerBase
{
    private readonly DataDbContext _dbContext = context;

    // GET: api/<ShipsController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ship>>> Get()
    {
        return Ok(await Task.FromResult(_dbContext.Ships
            .Include(s => s.Class)
            .Include(s => s.Outcomes)
            .ThenInclude(o => o.Battle)));
    }

    // GET api/<ShipsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Ship>> Get(int id)
    {
        var ship = await _dbContext.Ships
            .Include(s => s.Class)
            .Include(s => s.Outcomes)
            .ThenInclude(o => o.Battle)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (ship == null)
            return NotFound($"Ship with id={id} not found!");

        return Ok(ship);
    }

    // POST api/<ShipsController>
    [HttpPost]
    public async Task<ActionResult<Ship>> Post(Ship ship)
    {
        if (ship == null)
            return BadRequest("Ship is null!");

        if (await _dbContext.Ships.FindAsync(ship.Id) != null)
            return Problem($"Ship with id={ship.Id} already exists!");

        var newShip = await new Creator().GetModel(_dbContext, _dbContext.Ships, ship);

        await _dbContext.Ships.AddAsync(newShip);
        await _dbContext.SaveChangesAsync();

        return Ok(newShip);
    }

    // PUT api/<ShipsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ShipsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

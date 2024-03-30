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
public class BattlesController(DataDbContext dbContext) : ControllerBase
{
    // GET: api/<BattlesController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Battle>>> GetAllAsync() =>
        Ok(await Task.FromResult(dbContext.Battles));

    // GET api/<BattlesController>/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Battle>> GetAsync(int id)
    {
        var battle = await dbContext.Battles.FindAsync(id);

        if (battle is null)
            return NotFound($"Battle with id={id} not found!");

        return Ok(battle);
    }

    // POST api/<BattlesController>
    [HttpPost]
    public async Task<ActionResult<Battle>> PostAsync(Battle battle)
    {
        if (battle == null)
            return BadRequest("Battle is null!");

        if (await dbContext.Battles.FindAsync(battle.Id) is not null)
            return Problem($"Battle with id={battle.Id} already exists!");

        var newBattle = await new Factory<Battle>()
                        .GetModel(dbContext, dbContext.Battles, battle);

        await dbContext.Battles.AddAsync(newBattle);
        await dbContext.SaveChangesAsync();

        return Ok(newBattle);
    }

    // PUT api/<BattlesController>
    [HttpPut]
    public async Task<ActionResult<Battle>> PutAsync(Battle battle)
    {
        var findBattle = await dbContext.Battles.FindAsync(battle.Id);

        if (findBattle is null)
            return NotFound($"Battle with id={battle.Id} not found!");

        var updatedBattle = await new Factory<Battle>()
                            .UpdateModel(dbContext, dbContext.Battles, battle);
        await dbContext.SaveChangesAsync();

        return Ok(updatedBattle);
    }

    // DELETE api/<BattlesController>/id
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var battle = await dbContext.Battles.FindAsync(id);

        if (battle is null)
            return NotFound($"Battle with id={id} not found!");

        dbContext.Battles.Remove(battle);
        await dbContext.SaveChangesAsync();

        return Ok("Remove Completed!");
    }
}

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
public class OutcomesController(DataDbContext dbContext) : ControllerBase
{
    // GET: api/<OutcomesController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Outcome>>> Get() =>
        Ok(await Task.FromResult(dbContext.Outcomes
                                          .Include(o => o.Battle)));

    // GET api/<OutcomesController>/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Outcome>> Get(int id)
    {
        var outcome = await dbContext.Outcomes
                                     .Include(o => o.Battle)
                                     .FirstOrDefaultAsync(o => o.Id == id);

        if (outcome is null)
            return NotFound($"Outcome with id={id} not found!");

        return Ok(outcome);
    }

    // POST api/<OutcomesController>
    [HttpPost]
    public async Task<ActionResult<Outcome>> Post(Outcome outcome)
    {
        if (outcome == null)
            return BadRequest("Outcome is null!");

        if (await dbContext.Outcomes.FindAsync(outcome.Id) is not null)
            return Problem($"Outcome with id={outcome.Id} already exists!");

        var newOutcome = await new Factory<Outcome>()
                         .GetModel(dbContext, dbContext.Outcomes, outcome);

        await dbContext.Outcomes.AddAsync(newOutcome);
        await dbContext.SaveChangesAsync();

        return Ok(newOutcome);
    }

    // PUT api/<OutcomesController>
    [HttpPut]
    public async Task<ActionResult<Outcome>> Put(Outcome outcome)
    {
        var findOutcome = await dbContext.Outcomes.FindAsync(outcome.Id);

        if (findOutcome is null)
            return NotFound($"Outcome with id={outcome.Id} not found!");

        var updatedOutcome = await new Factory<Outcome>()
                             .UpdateModel(dbContext, dbContext.Outcomes, outcome);
        await dbContext.SaveChangesAsync();

        return Ok(updatedOutcome);
    }

    // DELETE api/<OutcomesController>/id
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var outcome = await dbContext.Outcomes.FindAsync(id);

        if (outcome is null)
            return NotFound($"Outcome with id={id} not found!");

        dbContext.Outcomes.Remove(outcome);
        await dbContext.SaveChangesAsync();

        return Ok("Remove Completed!");
    }
}

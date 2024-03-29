using ShipAccounting.Data;
using ShipAccounting.Models.CreatingModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShipAccounting.Models;

public class Ship : ICreatingModel<Ship>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public short? Launched { get; set; }
    public ShipClass? Class { get; set; }
    public List<Outcome>? Outcomes { get; set; }

    [JsonIgnore]
    public int GetId => Id;

    public async Task CreateModelAsync(DataDbContext dbContext, Ship ship)
    {
        Name = ship.Name;
        Launched = ship.Launched;

        if (ship.Class != null)
            Class = await new Creator().GetModel(dbContext, dbContext.Classes, ship.Class);

        if (ship.Outcomes != null)
            Outcomes = ship.Outcomes.Select(async o =>
                await new Creator().GetModel(dbContext, dbContext.Outcomes, o))
                ?.Select(t => t.Result)
                ?.ToList();
    }
}

using ShipAccounting.Data;
using ShipAccounting.Models.ModelsSources;
using ShipAccounting.Models.ModelsSources.ModelInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipAccounting.Models;

public class Ship : IFactoryModel<Ship>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public short? Launched { get; set; }
    public ShipClass? Class { get; set; }
    public List<Outcome>? Outcomes { get; set; }

    public async Task<Ship> GetUpdatedModel(DataDbContext dbContext, Ship model)
    {
        model.Name = Name;
        model.Launched = Launched;

        model.Class = Class is null ? null :
            await new Factory<ShipClass>()
            .GetModel(dbContext, dbContext.Classes, Class);

        if (model.Outcomes is not null && model.Outcomes.Count > 0)
            dbContext.Outcomes.RemoveRange(model.Outcomes);

        model.Outcomes = Outcomes?.Select(async o =>
            await new Factory<Outcome>()
            .GetModel(dbContext, dbContext.Outcomes, o))
            ?.Select(t => t.Result)
            .ToList();

        return await Task.FromResult(model);
    }
}

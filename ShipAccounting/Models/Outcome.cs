using ShipAccounting.Data;
using ShipAccounting.Models.ModelsSources;
using ShipAccounting.Models.ModelsSources.ModelInterfaces;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShipAccounting.Models;

public class Outcome : IFactoryModel<Outcome>
{
    public int Id { get; set; }
    public int ShipId { get; set; }
    public Battle Battle { get; set; }
    public Results? Result { get; set; }

    public async Task<Outcome> GetUpdatedModel(DataDbContext dbContext, Outcome model)
    {
        model.ShipId = ShipId;
        model.Battle = await new Factory<Battle>()
                       .GetModel(dbContext, dbContext.Battles, Battle);
        model.Result = Result;

        return await Task.FromResult(model);
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Results
{
    Ok,
    Damaged,
    Sunk
}

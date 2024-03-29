using ShipAccounting.Data;
using ShipAccounting.Models.CreatingModel;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShipAccounting.Models;

public class Outcome : ICreatingModel<Outcome>
{
    public int Id { get; set; }
    public int ShipId { get; set; }
    public Battle Battle { get; set; }
    public Results? Result { get; set; }

    [JsonIgnore]
    public int GetId => Id;

    public async Task CreateModelAsync(DataDbContext dbContext, Outcome outcome)
    {
        ShipId = outcome.ShipId;
        Battle = await new Creator().GetModel(dbContext, dbContext.Battles, outcome.Battle);
        Result = outcome.Result;
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Results
{
    Ok,
    Damaged,
    Sunk
}

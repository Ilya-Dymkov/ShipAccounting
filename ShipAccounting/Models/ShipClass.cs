using ShipAccounting.Data;
using ShipAccounting.Models.CreatingModel;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShipAccounting.Models;

public class ShipClass : ICreatingModel<ShipClass>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Type { get; set; }
    public string? Country { get; set; }
    public short? NumGuns { get; set; }
    public short? Bore { get; set; }
    public int? Displacement { get; set; }

    [JsonIgnore]
    public int GetId => Id;

    public Task CreateModelAsync(DataDbContext dbContext, ShipClass shipClass)
    {
        Name = shipClass.Name;
        Type = shipClass.Type;
        Country = shipClass.Country;
        NumGuns = shipClass.NumGuns;
        Bore = shipClass.Bore;
        Displacement = shipClass.Displacement;
        return Task.CompletedTask;
    }
}

using ShipAccounting.Data;
using ShipAccounting.Models.CreatingModel;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShipAccounting.Models;

public class Battle : ICreatingModel<Battle>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? Date { get; set; }

    [JsonIgnore]
    public int GetId => Id;

    public Task CreateModelAsync(DataDbContext dbContext, Battle battle)
    {
        Name = battle.Name;
        Date = battle.Date;
        return Task.CompletedTask;
    }
}

using ShipAccounting.Data;
using ShipAccounting.Models.ModelsSources.ModelInterfaces;
using System;
using System.Threading.Tasks;

namespace ShipAccounting.Models;

public class Battle : IFactoryModel<Battle>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? Date { get; set; }

    public Task<Battle> GetUpdatedModel(DataDbContext dbContext, Battle model)
    {
        model.Name = Name;
        model.Date = Date;

        return Task.FromResult(model);
    }
}

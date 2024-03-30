using ShipAccounting.Data;
using ShipAccounting.Models.ModelsSources.ModelInterfaces;
using System.Threading.Tasks;

namespace ShipAccounting.Models;

public class ShipClass : IFactoryModel<ShipClass>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Type { get; set; }
    public string? Country { get; set; }
    public short? NumGuns { get; set; }
    public short? Bore { get; set; }
    public int? Displacement { get; set; }

    public Task<ShipClass> GetUpdatedModel(DataDbContext dbContext, ShipClass model)
    {
        model.Name = Name;
        model.Type = Type;
        model.Country = Country;
        model.NumGuns = NumGuns;
        model.Bore = Bore;
        model.Displacement = Displacement;

        return Task.FromResult(model);
    }
}

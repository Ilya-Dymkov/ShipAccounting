using Microsoft.EntityFrameworkCore;
using ShipAccounting.Data;
using ShipAccounting.Models.ModelsSources.ModelInterfaces;
using System.Threading.Tasks;

namespace ShipAccounting.Models.ModelsSources;

public class Factory<TModel>
    where TModel : class, IFactoryModel<TModel>, new()
{
    public async Task<TModel> GetModel(DataDbContext dbContext, DbSet<TModel> models, TModel model) =>
        await models.FindAsync(model.Id) ?? await model.GetUpdatedModel(dbContext, new());

    public async Task<TModel> UpdateModel(DataDbContext dbContext, DbSet<TModel> models, TModel model) =>
        await model.GetUpdatedModel(dbContext, await models.FindAsync(model.Id) ?? new());
}

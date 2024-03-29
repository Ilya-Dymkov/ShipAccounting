using Microsoft.EntityFrameworkCore;
using ShipAccounting.Data;
using System.Threading.Tasks;

namespace ShipAccounting.Models.CreatingModel;

public class Creator
{
    public async Task<TModel> GetModel<TModel>(DataDbContext dbContext, DbSet<TModel> models, TModel model)
        where TModel : class, ICreatingModel<TModel>, new()
    {
        var newModel = await models.FindAsync(model.GetId) ?? new TModel();
        await newModel.CreateModelAsync(dbContext, model);
        return newModel;
    }
}

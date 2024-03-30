using ShipAccounting.Data;
using System.Threading.Tasks;

namespace ShipAccounting.Models.ModelsSources.ModelInterfaces;

public interface IFactoryModel<TModel> : IModel
    where TModel : class, IFactoryModel<TModel>, new()
{
    Task<TModel> GetUpdatedModel(DataDbContext dbContext, TModel model);
}

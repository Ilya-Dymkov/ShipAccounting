using ShipAccounting.Data;
using System.Threading.Tasks;

namespace ShipAccounting.Models.CreatingModel;

public interface ICreatingModel<TModel>
    where TModel : class, ICreatingModel<TModel>, new()
{
    int GetId { get; }
    Task CreateModelAsync(DataDbContext dbContext, TModel model);
}

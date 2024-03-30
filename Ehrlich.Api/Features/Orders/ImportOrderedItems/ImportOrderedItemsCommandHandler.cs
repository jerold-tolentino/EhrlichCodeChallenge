using EFCore.BulkExtensions;
using Ehrlich.Api.Database;
using Ehrlich.Api.Entities;
using Ehrlich.Api.Features.Orders.ImportOrderedItems;
using Ehrlich.Api.Features.PizzaTypes;
using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.Orders.ImportOrderedItems;

public class ImportOrderedItemsCommandHandler : IRequestHandler<ImportOrderedItemsCommand, Result<int>>
{
    private readonly ApplicationDbContext _dbContext;

    public ImportOrderedItemsCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<int>> Handle(ImportOrderedItemsCommand request, CancellationToken cancellationToken)
    {
        using(var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                if (!request.OrderedItems.Any())
                {
                    return Result.Failure<int>(Error.NullValue);
                }
                var pizzas = _dbContext.Pizzas.ToList();
                var orders = _dbContext.Orders.ToList();

                var orderedItems = new List<OrderedItem>();

                foreach(var row in request.OrderedItems)
                {
                    var order = orders.Find(o => o.OrderNumber == row.OrderId);
                    if(order is null)
                    {
                        return Result.Failure<int>(OrderErrors.OrderNotFound);
                    }

                    var pizza = pizzas.Find(p => p.UniqueCode == row.PizzaUniqueCode);
                    if (pizza is null)
                    {
                        return Result.Failure<int>(OrderErrors.PizzatypeNotFound);
                    }

                    var orderedItem = new OrderedItem
                    {
                        Id = row.Id,
                        Order = order,
                        Pizza = pizza
                    };
                    orderedItems.Add(orderedItem);
                };

                //Will Insert if Primary key does not exist, update otherwise
                await _dbContext.BulkInsertOrUpdateAsync(orderedItems);
                transaction.Commit();
                return Result.Success(request.OrderedItems.Count);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return Result.Failure<int>(new Error("Error.DatabaseExeption", ex.Message));
            }
        };
    }
}

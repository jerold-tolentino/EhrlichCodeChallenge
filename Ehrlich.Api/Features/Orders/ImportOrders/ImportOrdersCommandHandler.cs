using EFCore.BulkExtensions;
using Ehrlich.Api.Database;
using Ehrlich.Api.Entities;
using Ehrlich.Api.Features.PizzaTypes;
using Ehrlich.Api.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace Ehrlich.Api.Features.Orders.ImportOrders;

public class ImportOrdersCommandHandler : IRequestHandler<ImportOrdersCommand, Result<int>>
{
    private readonly ApplicationDbContext _dbContext;

    public ImportOrdersCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<int>> Handle(ImportOrdersCommand request, CancellationToken cancellationToken)
    {
        using(var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                if (!request.Orders.Any())
                {
                    return Result.Failure<int>(Error.NullValue);
                }
                var orders = new List<Order>();
                var existingOrders = await _dbContext.Orders.ToListAsync();
                foreach (var order in request.Orders)
                {
                    var existingOrder = existingOrders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);
                    if(existingOrder is not null)
                    {
                        continue;
                    }
                    orders.Add(order);
                }
                await _dbContext.Orders.AddRangeAsync(orders);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();   
                return Result.Success(request.Orders.Count);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return Result.Failure<int>(new Error("Error.DatabaseExeption", ex.Message));
            }
        };
    }
}

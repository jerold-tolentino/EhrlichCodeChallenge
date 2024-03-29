using Ehrlich.Api.Database;
using Ehrlich.Api.Entities;
using Ehrlich.Api.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ehrlich.Api.Features.Pizzas.ImportPizzas;

public class ImportPizzasCommandHandler : IRequestHandler<ImportPizzasCommand, Result<ImportPizzasResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public ImportPizzasCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ImportPizzasResponse>> Handle(
        ImportPizzasCommand request,
        CancellationToken cancellationToken)
    {
        var pizzas = new List<Pizza>();
        foreach (var pizza in request.Pizzas)
        {
            var type = await _dbContext.PizzaTypes
                .Include(p => p.Ingredients)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(s => s.UniqueCode == pizza.PizzaTypeUniqueCode);

            if (type is null)
            {
                return Result.Failure<ImportPizzasResponse>(PizzaErrors.PizzaTypeNotFound);
            }

            var existingPizza = await _dbContext.Pizzas.FirstOrDefaultAsync( p => p.UniqueCode == pizza.UniqueCode);
            if (existingPizza is not null)
            {
                existingPizza.UniqueCode = pizza.UniqueCode;
                existingPizza.Type = type;
                existingPizza.Size = pizza.Size;
                existingPizza.Price = pizza.Price;
                pizzas.Add(existingPizza);
                continue;
            }

            var newPizza = new Pizza
            {
                UniqueCode = pizza.UniqueCode,
                Type = type,
                Size = pizza.Size,
                Price = pizza.Price
            };
            pizzas.Add(newPizza);

            await _dbContext.Pizzas.AddAsync(newPizza);
        }
        await _dbContext.SaveChangesAsync();

        var response = new ImportPizzasResponse(pizzas);
        return Result.Success<ImportPizzasResponse>(response);
    }
}

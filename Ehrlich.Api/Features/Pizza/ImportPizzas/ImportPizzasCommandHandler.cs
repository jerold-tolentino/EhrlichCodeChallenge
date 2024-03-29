using Ehrlich.Api.Database;
using Ehrlich.Api.Shared;
using MediatR;
using PizzaEntity = Ehrlich.Api.Entities.Pizza;

namespace Ehrlich.Api.Features.Pizza.ImportPizzas;

public class ImportPizzasCommandHandler : IRequestHandler<ImportPizzasCommand, Result<List<PizzaEntity>>>
{
    private readonly ApplicationDbContext _dbContext;

    public ImportPizzasCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<PizzaEntity>>> Handle(
        ImportPizzasCommand request,
        CancellationToken cancellationToken)
    {
        var pizzas = new List<PizzaEntity>();
        foreach (var pizza in request.Pizzas)
        {
            var type = await _dbContext.PizzaTypes.FindAsync(pizza.Type);

            if (type is null)
            {
                return Result.Failure<List<PizzaEntity>>(PizzaErrors.PizzaTypeNotFound);
            }

            //var existingPizza = await _dbContext.Pizzas.FindAsync(pizza.Id);
            //if (existingPizza is not null)
            //{
            //    return Result.Failure<List<PizzaEntity>>(PizzaErrors.PizzaAlreadyExist);
            //}

            var newPizza = new PizzaEntity
            {
                Type = type,
                Size = pizza.Size,
                Price = pizza.Price
            };
            pizzas.Add(newPizza);
        }
        await _dbContext.Pizzas.AddRangeAsync(pizzas);
        await _dbContext.SaveChangesAsync();

        return pizzas;
    }
}

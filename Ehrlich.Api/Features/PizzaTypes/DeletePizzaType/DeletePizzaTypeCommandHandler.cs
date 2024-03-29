using Ehrlich.Api.Database;
using Ehrlich.Api.Features.PizzaTypes.DeletePizzaType;
using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.PizzaTypes.CreatePizzaType;

public class DeletePizzaTypeCommandHandler : IRequestHandler<DeletePizzaTypeCommand, Result<bool>>
{
    private readonly ApplicationDbContext _dbContext;

    public DeletePizzaTypeCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<bool>> Handle(
        DeletePizzaTypeCommand request,
        CancellationToken cancellationToken)
    {
        var pizzaType = await _dbContext.PizzaTypes.FindAsync(request.PizzaTypeId);

        if (pizzaType is null)
        {
            return Result.Failure<bool>(PizzaTypeErrors.NotFound);
        }

        _dbContext.PizzaTypes.Remove(pizzaType);
        await _dbContext.SaveChangesAsync();

        return Result.Success(true);
    }
}

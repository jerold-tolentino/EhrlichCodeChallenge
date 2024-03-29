using Ehrlich.Api.Database;
using Ehrlich.Api.Entities;
using Ehrlich.Api.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ehrlich.Api.Features.PizzaTypes.CreatePizzaType;

public class CreatePizzaTypeCommandHandler : IRequestHandler<CreatePizzaTypeCommand, Result<CreatePizzaTypeResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public CreatePizzaTypeCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CreatePizzaTypeResponse>> Handle(
        CreatePizzaTypeCommand request,
        CancellationToken cancellationToken)
    {
        var pizzaType = await _dbContext.PizzaTypes
            .FirstOrDefaultAsync(c => c.Name.ToLower() == request.Name.ToLower());

        if (pizzaType is not null)
        {
            return Result.Failure<CreatePizzaTypeResponse>(PizzaTypeErrors.AlreadyExist);
        }

        pizzaType = new PizzaType
        {
            Name = request.Name,
            Category = await GetPizzaCategoryAsync(request.Category),
            Ingredients = await GetIngredientsAsync(request.Ingredients),
        };

        await _dbContext.PizzaTypes.AddAsync(pizzaType);
        await _dbContext.SaveChangesAsync();

        var response = new CreatePizzaTypeResponse(
            pizzaType.Id,
            pizzaType.Name,
            pizzaType.Category,
            pizzaType.Ingredients.ToList());

        return Result.Success(response);
    }

    private async Task<PizzaCategory> GetPizzaCategoryAsync(string categoryName)
    {
        var category = await _dbContext.PizzaCategories
            .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());

        if (category is null)
        {
            //Another use-case is to return an error when something is not existing
            //return Result.Failure<CreatePizzaTypeResponse>(PizzaTypeErrors.CategoryNotFound);

            category = new PizzaCategory
            {
                Name = categoryName,
            };
        }
        return category;
    }

    private async Task<List<Ingredient>> GetIngredientsAsync(string[] ingredientNames)
    {
        var ingredients = await _dbContext.Ingredients
            .Where(c => ingredientNames.Contains(c.Name))
            .ToListAsync();

        if (ingredients.Count < ingredientNames.Length)
        {
            var newIngredients = ingredientNames
                .Where(name => ingredients.Find(ing => ing.Name == name) is null);
            foreach (var ingredient in newIngredients)
            {
                var newIngredient = new Ingredient
                {
                    Name = ingredient,
                };
                ingredients.Add(newIngredient);
            }
        }
        return ingredients;
    }
}

using Ehrlich.Api.Database;
using Ehrlich.Api.Entities;
using Ehrlich.Api.Features.PizzaTypes.ImportPizzaTypes;
using Ehrlich.Api.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;

public class ImportPizzaTypesCommandHandler : IRequestHandler<ImportPizzaTypesCommand, Result<ImpotPizzaTypesResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public ImportPizzaTypesCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ImpotPizzaTypesResponse>> Handle(
        ImportPizzaTypesCommand request,
        CancellationToken cancellationToken)
    {
        var pizzaTypes = new List<PizzaType>();
        foreach (var type in request.PizzaTypes)
        {
            var pizzaType = await _dbContext.PizzaTypes.FirstOrDefaultAsync(p => p.UniqueCode == type.UniqueCode);

            if (pizzaType is not null)
            {
                pizzaType.Name = type.Name;
                pizzaType.Category = await GetCategoryAsync(type.Category);
                pizzaType.Ingredients = await GetIngredientsAsync(type.Ingredients);
                pizzaTypes.Add(pizzaType);
                continue;
            }

            var newPizzaType = new PizzaType
            {
                Name = type.Name,
                UniqueCode = type.UniqueCode,
                Category = await GetCategoryAsync(type.Category),
                Ingredients = await GetIngredientsAsync(type.Ingredients)
            };
            pizzaTypes.Add(newPizzaType);
            await _dbContext.PizzaTypes.AddAsync(newPizzaType);
        }

        await _dbContext.SaveChangesAsync();

        var response = new ImpotPizzaTypesResponse(pizzaTypes);
        return Result.Success<ImpotPizzaTypesResponse>(response);
    }

    private async Task<PizzaCategory> GetCategoryAsync(string categoryName)
    {
        var category = await _dbContext
            .PizzaCategories
            .FirstOrDefaultAsync(d => 
                d.Name.ToLower() == categoryName.Trim().ToLower());

        if (category is not null)
        {
            return category!;
        }

        var newCategory = new PizzaCategory
        {
            Name = categoryName
        };
        _dbContext.PizzaCategories.Add(newCategory);

        _dbContext.SaveChanges();
        return newCategory;
    }

    private async Task<List<Ingredient>> GetIngredientsAsync(string ingredients)
    {
        var ingredientList = new List<Ingredient>();

        var arrayOfIngredients = ingredients.Replace("\"",string.Empty).Split(",");

        foreach( var ingredientName in  arrayOfIngredients )
        {
            var ingredient = await GetIngredientAsync(ingredientName);
            ingredientList.Add(ingredient);
        }
        return ingredientList;
    }

    private async Task<Ingredient> GetIngredientAsync(string ingredientName)
    {
        var ingredient = await _dbContext
            .Set<Ingredient>()
            .FirstOrDefaultAsync(d =>
                d.Name.ToLower() == ingredientName.Trim().ToLower());

        if (ingredient is not null)
        {
            return ingredient!;
        }

        var newIngredient = new Ingredient
        {
            Name = ingredientName.Trim()
        };
        _dbContext.Set<Ingredient>().Add(newIngredient);

        _dbContext.SaveChanges();
        return newIngredient;
    }
}

using Ehrlich.Api.Database;
using Ehrlich.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;

public class ImportPizzaTypesCommandHandler : IRequestHandler<ImportPizzaTypesCommand, int>
{
    private readonly ApplicationDbContext _dbContext;

    public ImportPizzaTypesCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(
        ImportPizzaTypesCommand request,
        CancellationToken cancellationToken)
    {
        foreach (var type in request.PizzaTypes)
        {
            var pizzaType = await _dbContext.PizzaTypes.FindAsync(type.Id);

            if (pizzaType is not null)
            {
                continue;
            }

            var newPizzaType = new PizzaType
            {
                Name = type.Name,
                Category = await GetCategoryAsync(type.Category),
                Ingredients = await GetIngredientsAsync(type.Ingredients)
            };
            _dbContext.PizzaTypes.Add(newPizzaType);
        }
        _dbContext.SaveChanges();
        return request.PizzaTypes.Count;
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

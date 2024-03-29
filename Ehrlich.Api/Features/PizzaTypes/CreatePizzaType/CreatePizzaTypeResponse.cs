using Ehrlich.Api.Entities;

namespace Ehrlich.Api.Features.PizzaTypes.CreatePizzaType;

public record CreatePizzaTypeResponse(
    int Id,
    string Name,
    PizzaCategory Category,
    List<Ingredient> Ingredients);

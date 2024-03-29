using Ehrlich.Api.Entities;

namespace Ehrlich.Api.Features.PizzaTypes.GetPizzaType;

public record GetPizzaTypeResponse(
    int Id,
    string Name,
    PizzaCategory Category,
    List<Ingredient> Ingredients);

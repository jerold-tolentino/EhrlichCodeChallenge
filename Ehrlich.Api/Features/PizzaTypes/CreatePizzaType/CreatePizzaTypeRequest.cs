namespace Ehrlich.Api.Features.PizzaTypes.CreatePizzaType;

public record CreatePizzaTypeRequest(
    string Name,
    string Category,
    string[] Ingredients);

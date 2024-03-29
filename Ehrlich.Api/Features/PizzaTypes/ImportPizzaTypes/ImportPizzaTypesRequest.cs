namespace Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;

public record ImportPizzaTypesRequest(
    string? UniqueCode,
    string Name,
    string Category,
    string Ingredients);

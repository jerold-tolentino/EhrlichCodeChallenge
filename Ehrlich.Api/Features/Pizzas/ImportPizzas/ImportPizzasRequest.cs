using Ehrlich.Api.Shared.Enumerations;

namespace Ehrlich.Api.Features.Pizzas.ImportPizzas;

public record ImportPizzasRequest(
    string? UniqueCode,
    Size Size,
    string PizzaTypeUniqueCode,
    decimal Price);

using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Features.Pizzas;

public static class PizzaErrors
{
    public static readonly Error PizzaTypeNotFound = new("Error.PizzaTypeNotFound", "Pizza Type does not exist");
    public static readonly Error PizzaAlreadyExist = new("Error.PizzaAlreadyExist", "Pizza with similar Id already exist");
}

using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Features.Pizza.ImportPizzas;

public record PizzaErrors(string Code, string Message) : Error(Code, Message)
{
    public static readonly Error PizzaTypeNotFound = new("Error.PizzaTypeNotFound", "Pizza Type does not exist");
    public static readonly Error PizzaAlreadyExist = new("Error.PizzaAlreadyExist", "Pizza with similar Id already exist");
}

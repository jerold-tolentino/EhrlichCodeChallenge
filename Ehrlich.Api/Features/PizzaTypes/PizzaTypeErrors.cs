using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Features.PizzaTypes;

public static class PizzaTypeErrors
{
    public static readonly Error CategoryNotFound = new("Error.CategoryNotFound", "Category does not exist");
    public static readonly Error PizzaTypeAlreadyExist = new("Error.PizzaTypeAlreadyExist", "Pizza type with similar name already exist");
}

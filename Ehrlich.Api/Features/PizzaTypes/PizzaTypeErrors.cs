using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Features.PizzaTypes;

public static class PizzaTypeErrors
{
    private static readonly Error CategoryNotFound = new("Error.CategoryNotFound", "Category does not exist");
}

using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Features.PizzaTypes;

public static class PizzaTypeErrors
{
    public static readonly Error CategoryNotFound = new("PizzaType.CategoryNotFound", "Category does not exist");
    public static readonly Error AlreadyExist = new("PizzaType.AlreadyExist", "Pizza type with similar name already exist");
    public static readonly Error NotFound = new("PizzaType.NotFound", "Pizza type does not exist");
}

using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Features.PizzaTypes;

public static class OrderErrors
{
    public static readonly Error DatabaseError = new("Order.DatabaseError", "Encountered some error");
    public static readonly Error OrderNotFound = new("Order.OrderNotFound", "Related Order record does not exist");
    public static readonly Error PizzatypeNotFound = new("Order.PizzatypeNotFound", "Related Pizza Type record does not exist");
}

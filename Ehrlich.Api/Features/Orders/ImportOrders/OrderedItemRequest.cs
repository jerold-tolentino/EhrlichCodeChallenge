namespace Ehrlich.Api.Features.Orders.ImportOrders;

public record OrderedItemRequest(
    int Id,
    int OrderId,
    string PizzaUniqueCode);

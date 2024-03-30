using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Entities;

public class OrderedItem : Entity
{
    public int OrderedItemId { get; set; }
    public Order Order { get; set; } = null!;
    public Pizza Pizza { get; set; } = null!;
    public int Quantity { get; set; }
}

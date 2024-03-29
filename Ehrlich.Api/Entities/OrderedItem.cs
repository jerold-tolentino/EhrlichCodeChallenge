using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Entities;

public class OrderedItem : Entity
{
    public Pizza Pizza { get; set; } = null!;
    public int Quantity { get; set; }
}

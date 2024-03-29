using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Entities;

public class Order : Entity
{
    public virtual ICollection<OrderedItem> Items { get; set; } = new List<OrderedItem>();
    public DateTime OrderDate { get; set; } 
}

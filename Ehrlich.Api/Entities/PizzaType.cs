using Ehrlich.Api.Shared;

namespace Ehrlich.Api.Entities;

public class PizzaType : Entity
{
    public string? UniqueCode { get; set; }
    public string Name { get; set; } = null!;
    public PizzaCategory Category { get; set; } = null!;
    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}

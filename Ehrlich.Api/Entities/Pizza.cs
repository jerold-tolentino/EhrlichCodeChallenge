using Ehrlich.Api.Shared;
using Ehrlich.Api.Shared.Enumerations;

namespace Ehrlich.Api.Entities;

public class Pizza : Entity
{
    public string? UniqueCode { get; set; }
    public PizzaType Type { get; set; } = null!;
    public Size Size { get; set; }
    public decimal Price { get; set; }
}

using Ehrlich.Api.Shared.Enumerations;

namespace Ehrlich.Api.Features.Pizza.ImportPizzas;

public class ImportPizzasRequest
{
    public string? UniqueCode { get; set; }
    public Size Size { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
}

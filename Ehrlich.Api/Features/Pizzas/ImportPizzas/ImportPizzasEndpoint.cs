using Ehrlich.Api.Features.Pizzas.ImportPizzas;
using Ehrlich.Api.Shared.Enumerations;
using Microsoft.AspNetCore.Mvc;
using nietras.SeparatedValues;

namespace Ehrlich.Api.Features.Pizzas;

public partial class PizzasController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> ImportPizzas([FromForm(Name = "File")] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using var memoryStream = new MemoryStream(new byte[file.Length]);
        file.CopyTo(memoryStream);
        memoryStream.Position = 0;

        var records = await GetRecords(memoryStream);

        var command = new ImportPizzasCommand(records);
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: result.Error.Message);
        }

        return Ok(result.Value);
    }

    private Task<List<ImportPizzasRequest>> GetRecords(MemoryStream memoryStream)
    {
        var records = new List<ImportPizzasRequest>();
        using (var stream = new StreamReader(memoryStream))
        using (var sepReader = Sep.Reader().From(stream))
        {
            foreach (var row in sepReader)
            {
                if(!Enum.TryParse(row[2].ToString(), out Size size))
                {
                    continue;
                }

                var request = new ImportPizzasRequest(
                    UniqueCode: row[0].ToString(),
                    PizzaTypeUniqueCode: row[1].ToString(),
                    Size: size,
                    Price: Convert.ToDecimal(row[3].ToString()));

                records.Add(request);
            }
        }
        return Task.FromResult(records);
    }
}

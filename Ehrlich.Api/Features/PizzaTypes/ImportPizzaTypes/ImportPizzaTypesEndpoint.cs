using Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;
using Microsoft.AspNetCore.Mvc;
using nietras.SeparatedValues;

namespace Ehrlich.Api.Features.PizzaTypes;


public partial class PizzaTypesController : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> ImportPizzaTypes([FromForm(Name = "File")] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using var memoryStream = new MemoryStream(new byte[file.Length]);
        file.CopyTo(memoryStream);
        memoryStream.Position = 0;

        var records = await GetRecords(memoryStream);

        var command = new ImportPizzaTypesCommand(records);
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: result.Error.Message);
        }

        return Ok(result.Value);
    }

    private Task<List<ImportPizzaTypesRequest>> GetRecords(MemoryStream memoryStream)
    {
        var records = new List<ImportPizzaTypesRequest>();
        using (var stream = new StreamReader(memoryStream))
        using (var sepReader = Sep.Reader().From(stream))
        {
            foreach (var row in sepReader)
            {
                var model = new ImportPizzaTypesRequest(
                    UniqueCode: row[0].ToString(),
                    Name: row[1].ToString(),
                    Category: row[2].ToString(),
                    Ingredients: row[3].ToString());
                records.Add(model);
            }
        }
        return Task.FromResult(records);
    }
}

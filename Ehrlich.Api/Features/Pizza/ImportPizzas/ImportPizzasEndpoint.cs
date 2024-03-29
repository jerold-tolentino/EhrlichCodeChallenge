using Ehrlich.Api.Features.Pizza.ImportPizzas;
using Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;
using Ehrlich.Api.Shared.Enumerations;
using Microsoft.AspNetCore.Mvc;
using nietras.SeparatedValues;
using System.Formats.Asn1;
using System.Reflection.PortableExecutable;

namespace Ehrlich.Api.Features.Pizza;


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

        return Ok(result);
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

                var model = new ImportPizzasRequest
                {
                    //Id = row[0].ToString(),
                    Type = row[1].ToString(),
                    Size = size,
                    Price = Convert.ToDecimal(row[3].ToString()),
                };
                records.Add(model);
            }
        }
        return Task.FromResult(records);
    }
}

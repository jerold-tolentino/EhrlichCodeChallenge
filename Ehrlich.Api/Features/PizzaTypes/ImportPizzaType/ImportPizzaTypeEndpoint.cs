﻿using Azure;
using Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;
using Microsoft.AspNetCore.Mvc;
using nietras.SeparatedValues;
using System.Formats.Asn1;
using System.Reflection.PortableExecutable;

namespace Ehrlich.Api.Features.PizzaTypes;


public partial class PizzaTypeController : ControllerBase
{
    [HttpPost]
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

        return Ok(result);
    }

    private Task<List<PizzaTypeRequest>> GetRecords(MemoryStream memoryStream)
    {
        var records = new List<PizzaTypeRequest>();
        using (var stream = new StreamReader(memoryStream))
        using (var sepReader = Sep.Reader().From(stream))
        {
            foreach (var row in sepReader)
            {
                var model = new PizzaTypeRequest
                {
                    Id = row[0].ToString(),
                    Name = row[1].ToString(),
                    Category = row[2].ToString(),
                    Ingredients = row[3].ToString(),
                };
                records.Add(model);
            }
        }
        return Task.FromResult(records);
    }
}
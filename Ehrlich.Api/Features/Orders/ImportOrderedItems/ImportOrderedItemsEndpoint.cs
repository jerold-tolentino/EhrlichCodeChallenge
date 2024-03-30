using BenchmarkDotNet.Attributes;
using Ehrlich.Api.Database;
using Ehrlich.Api.Entities;
using Ehrlich.Api.Features.Orders.ImportOrderedItems;
using Ehrlich.Api.Features.Orders.ImportOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using nietras.SeparatedValues;
using System.Globalization;

namespace Ehrlich.Api.Features.Orders;

public partial class OrdersController : ControllerBase
{
    [HttpPost("import-ordered-items")]
    public async Task<IActionResult> ImportOrderedItems([FromForm(Name = "Items")] IFormFile item)
    {
        if (item == null || item.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using var memoryStream = new MemoryStream(new byte[item.Length]);
        item.CopyTo(memoryStream);
        memoryStream.Position = 0;

        var records = await GetOrderedItem(memoryStream);

        var command = new ImportOrderedItemsCommand(records);
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: result.Error.Message);
        }
        return Ok(new
        {
            message = $"Successfully imported {result.Value} ordered items"
        });
    }

    private Task<List<OrderedItemRequest>> GetOrderedItem(MemoryStream memoryStream)
    {
        using (var stream = new StreamReader(memoryStream))
        using (var sepReader = Sep.Reader().From(stream))
        {
            var records = sepReader.ParallelEnumerate(row =>
            {
                var request = new OrderedItemRequest(
                    Id: row[0].Parse<int>(),
                    OrderId: row[1].Parse<int>(),
                    PizzaUniqueCode: row[2].Parse<string>());
                return request;
            }).ToList();
            return Task.FromResult(records);
        }
    }
}

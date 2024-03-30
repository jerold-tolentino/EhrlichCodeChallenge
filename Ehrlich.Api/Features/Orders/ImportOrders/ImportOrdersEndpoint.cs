using BenchmarkDotNet.Attributes;
using Ehrlich.Api.Database;
using Ehrlich.Api.Entities;
using Ehrlich.Api.Features.Orders.ImportOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using nietras.SeparatedValues;
using System.Globalization;

namespace Ehrlich.Api.Features.Orders;

public partial class OrdersController : ControllerBase
{
    [HttpPost("ImportOrders")]
    public async Task<IActionResult> ImportOrders([FromForm(Name = "Orders")] IFormFile orders)
    {
        if (orders == null || orders.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using var memoryStream = new MemoryStream(new byte[orders.Length]);
        orders.CopyTo(memoryStream);
        memoryStream.Position = 0;

        var records = await GetOrders(memoryStream);

        var command = new ImportOrdersCommand(records);
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: result.Error.Message);
        }
        return Ok(new
        {
            message = $"Successfully imported {result.Value} orders"
        });
    }

    private Task<List<Order>> GetOrders(MemoryStream memoryStream)
    {
        using (var stream = new StreamReader(memoryStream))
        using (var sepReader = Sep.Reader().From(stream))
        {
            var records = sepReader.ParallelEnumerate(row =>
            {
                var dateInString = $"{row[1].ToString()} {row[2].ToString()}";

                var orderDate = DateTime.ParseExact(dateInString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                var request = new Order
                {
                    OrderNumber = row[0].Parse<int>(),
                    OrderDate = orderDate
                };
                return request;
            }).ToList();
            return Task.FromResult(records);
        }
    }
}

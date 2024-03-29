using Ehrlich.Api.Features.PizzaTypes.DeletePizzaType;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.Api.Features.PizzaTypes;


public partial class PizzaTypesController : ControllerBase
{
    [HttpGet("{pizzaTypeId:int}")]
    public async Task<IActionResult> GetPizzaType(int pizzaTypeId)
    {
        var query = new GetPizzaTypeQuery(pizzaTypeId);

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: result.Error.Message,
                title: result.Error.Code);
        }

        return Ok(result.Value);
    }
}

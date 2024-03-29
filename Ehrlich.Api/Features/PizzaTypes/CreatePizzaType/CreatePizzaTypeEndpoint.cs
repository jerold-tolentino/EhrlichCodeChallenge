using Ehrlich.Api.Features.PizzaTypes.CreatePizzaType;
using Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;
using Microsoft.AspNetCore.Mvc;
using nietras.SeparatedValues;

namespace Ehrlich.Api.Features.PizzaTypes;


public partial class PizzaTypesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePizzaType(CreatePizzaTypeRequest request)
    {
        var command = new CreatePizzaTypeCommand(
            request.Name,
            request.Category,
            request.Ingredients);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: result.Error.Message);
        }

        return Ok(result.Value);
    }
}

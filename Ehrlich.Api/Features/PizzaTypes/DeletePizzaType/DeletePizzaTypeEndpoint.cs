using Ehrlich.Api.Features.PizzaTypes.DeletePizzaType;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.Api.Features.PizzaTypes;


public partial class PizzaTypesController : ControllerBase
{
    [HttpDelete]
    public async Task<IActionResult> DeletePizzaType(int pizzaTypeId)
    {
        var command = new DeletePizzaTypeCommand(pizzaTypeId);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: result.Error.Message,
                title: result.Error.Code);
        }

        return Ok(new
        {
            message = "Deleted Successfully",
            Id = pizzaTypeId
        });
    }
}

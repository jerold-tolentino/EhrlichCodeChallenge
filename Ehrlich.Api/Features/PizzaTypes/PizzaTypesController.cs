using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.Api.Features.PizzaTypes;


[ApiController]
[Route("api/[controller]")]
public partial class PizzaTypesController : ControllerBase
{
    private readonly ISender _sender;

    public PizzaTypesController(ISender sender)
    {
        _sender = sender;
    }
}

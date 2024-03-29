using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.Api.Features.PizzaTypes;


[ApiController]
[Route("api/[controller]")]
public partial class PizzaTypeController : ControllerBase
{
    private readonly ISender _sender;

    public PizzaTypeController(ISender sender)
    {
        _sender = sender;
    }
}

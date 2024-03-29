using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.Api.Features.Pizza;


[ApiController]
[Route("api/[controller]")]
public partial class PizzasController : ControllerBase
{
    private readonly ISender _sender;

    public PizzasController(ISender sender)
    {
        _sender = sender;
    }
}

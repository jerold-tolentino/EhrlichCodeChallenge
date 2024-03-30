using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.Api.Features.Orders;

[ApiController]
[Route("api/[controller]")]
public partial class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }
}

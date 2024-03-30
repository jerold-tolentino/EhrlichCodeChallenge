using Ehrlich.Api.Entities;
using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.Orders.ImportOrders;

public record ImportOrdersCommand(List<Order> Orders) : IRequest<Result<int>>;

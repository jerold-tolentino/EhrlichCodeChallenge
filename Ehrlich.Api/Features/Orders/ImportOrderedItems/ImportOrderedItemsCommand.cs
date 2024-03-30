using Ehrlich.Api.Entities;
using Ehrlich.Api.Features.Orders.ImportOrders;
using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.Orders.ImportOrderedItems;

public record ImportOrderedItemsCommand(List<OrderedItemRequest> OrderedItems) : IRequest<Result<int>>;

using Ehrlich.Api.Entities;
using Ehrlich.Api.Features.PizzaTypes.GetPizzaType;
using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.PizzaTypes.DeletePizzaType;

public record GetPizzaTypeQuery(int PizzaTypeId) : IRequest<Result<GetPizzaTypeResponse>>;

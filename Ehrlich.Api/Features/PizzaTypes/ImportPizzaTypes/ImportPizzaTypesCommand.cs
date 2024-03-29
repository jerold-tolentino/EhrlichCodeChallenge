using Ehrlich.Api.Features.PizzaTypes.ImportPizzaTypes;
using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;

public record ImportPizzaTypesCommand(List<ImportPizzaTypesRequest> PizzaTypes) : IRequest<Result<ImpotPizzaTypesResponse>>;

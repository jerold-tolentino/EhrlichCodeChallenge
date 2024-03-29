using MediatR;

namespace Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;

public record ImportPizzaTypesCommand(List<PizzaTypeRequest> PizzaTypes) : IRequest<int>;

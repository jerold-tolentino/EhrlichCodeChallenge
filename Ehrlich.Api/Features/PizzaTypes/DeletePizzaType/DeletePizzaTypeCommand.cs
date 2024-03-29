using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.PizzaTypes.DeletePizzaType;

public record DeletePizzaTypeCommand(int PizzaTypeId) : IRequest<Result<bool>>;

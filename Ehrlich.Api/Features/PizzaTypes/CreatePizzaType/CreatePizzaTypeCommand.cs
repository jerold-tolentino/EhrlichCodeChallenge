using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.PizzaTypes.CreatePizzaType;

public record CreatePizzaTypeCommand(
    string Name,
    string Category,
    string[] Ingredients) : IRequest<Result<CreatePizzaTypeResponse>>;

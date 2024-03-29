using Ehrlich.Api.Shared;
using MediatR;

namespace Ehrlich.Api.Features.Pizzas.ImportPizzas;

public record ImportPizzasCommand(List<ImportPizzasRequest> Pizzas)
    : IRequest<Result<ImportPizzasResponse>>;

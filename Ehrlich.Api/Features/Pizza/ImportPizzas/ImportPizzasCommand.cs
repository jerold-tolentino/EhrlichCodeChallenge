using Ehrlich.Api.Features.PizzaTypes.ImportPizzaType;
using Ehrlich.Api.Shared;
using MediatR;
using PizzaEntity = Ehrlich.Api.Entities.Pizza;

namespace Ehrlich.Api.Features.Pizza.ImportPizzas;

public record ImportPizzasCommand(List<ImportPizzasRequest> Pizzas) 
    : IRequest<Result<List<PizzaEntity>>>;

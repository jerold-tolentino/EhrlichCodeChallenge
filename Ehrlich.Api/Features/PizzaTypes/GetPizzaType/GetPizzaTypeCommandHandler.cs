﻿using Ehrlich.Api.Database;
using Ehrlich.Api.Features.PizzaTypes.DeletePizzaType;
using Ehrlich.Api.Features.PizzaTypes.GetPizzaType;
using Ehrlich.Api.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ehrlich.Api.Features.PizzaTypes.CreatePizzaType;

public class GetPizzaTypeCommandHandler : IRequestHandler<GetPizzaTypeQuery, Result<GetPizzaTypeResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetPizzaTypeCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetPizzaTypeResponse>> Handle(
        GetPizzaTypeQuery request,
        CancellationToken cancellationToken)
    {
        var pizzaType = await _dbContext.PizzaTypes
            .Include(p => p.Category)
            .Include(p => p.Ingredients)
            .FirstOrDefaultAsync(p => p.Id == request.PizzaTypeId);

        if (pizzaType is null)
        {
            return Result.Failure<GetPizzaTypeResponse>(PizzaTypeErrors.NotFound);
        }

        var pizzas = await _dbContext.Pizzas.Where(p => p.Type == pizzaType).ToListAsync();

        var response = new GetPizzaTypeResponse(
            Id: pizzaType.Id,
            Name: pizzaType.Name,
            Category: pizzaType.Category,
            Ingredients: pizzaType.Ingredients.ToList());

        return Result.Success(response);
    }
}

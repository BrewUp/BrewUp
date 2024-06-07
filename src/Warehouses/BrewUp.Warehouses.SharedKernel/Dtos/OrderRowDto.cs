using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;

namespace BrewUp.Warehouses.SharedKernel.Dtos;

public record OrderRowDto(BeerId BeerId, BeerName BeerName, Quantity Quantity);
using BrewUp.Warehouses.Domain.Entities;
using BrewUp.Warehouses.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class WithdrawalBySalesOrderCommandHandler : CommandHandlerBaseAsync<WithdrawalBySalesOrder>
{
    public WithdrawalBySalesOrderCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(WithdrawalBySalesOrder command, CancellationToken cancellationToken = default)
    {
        var aggregate = await Repository.GetByIdAsync<BeerAvailability>(command.BeerId.Value);
        aggregate.WithdrawalBeer(command.Quantity, command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}
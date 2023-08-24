using SequentialOutbox.Domain.Entities;
using SequentialOutbox.Domain.Repositories;
using Wolverine.Attributes;

namespace SequentialOutbox.Application.Commands.Handlers;

public static class CreateOrderCommandHandler
{
    [Transactional]
    public static async Task Handle(CreateOrderCommand command, 
        CancellationToken cancellationToken,
        IOrderRepository orderRepository)
    {
        var items = command.Items.Select(i => new OrderItem(i.ProductId, i.Quantity, i.UnitPrice));

        var order = new Order(command.Email, command.FirstAddressLine, command.SecondAddressLine, items);

        await orderRepository.AddAsync(order, cancellationToken);
    }
}
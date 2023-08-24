using SequentialOutbox.Domain.Repositories;

namespace SequentialOutbox.Application.Commands.Handlers;

public static class MarkOrderAsPaidCommandHandler
{
    public static async Task HandleAsync(MarkOrderAsPaidCommand command,
        CancellationToken cancellationToken,
        IOrderRepository orderRepository)
    {
        var order = await orderRepository.FindAsync(command.OrderId, cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException("Order was not found");
        }
        
        order.MarkAsPaid();

        await orderRepository.UpdateAsync(order, cancellationToken);
    }
}
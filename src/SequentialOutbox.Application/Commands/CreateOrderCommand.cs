using SequentialOutbox.Application.Dtos;

namespace SequentialOutbox.Application.Commands;

public record CreateOrderCommand
{
    public required string Email { get; init; }
    
    public required string FirstAddressLine { get; init; }
    
    public required string SecondAddressLine { get; init; }
    
    public required OrderItemDto[] Items { get; init; }
}
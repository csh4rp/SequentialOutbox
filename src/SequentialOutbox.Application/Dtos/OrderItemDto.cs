namespace SequentialOutbox.Application.Dtos;

public record OrderItemDto(long ProductId, int Quantity, decimal UnitPrice);
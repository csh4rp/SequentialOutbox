namespace EStore.Contracts.Dtos;

public record OrderItemDto(long ProductId, int Quantity, decimal UnitPrice);
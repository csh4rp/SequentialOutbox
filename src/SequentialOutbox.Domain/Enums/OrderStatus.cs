namespace SequentialOutbox.Domain.Enums;

public enum OrderStatus
{
    New = 1,
    
    Paid = 2,
    
    Prepared = 3,
    
    InDelivery = 4,
    
    Delivered = 5,
}
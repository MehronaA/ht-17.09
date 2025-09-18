using System;

namespace Domain.DTOs.OrderItems;

public class OrderItemCreateDto
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
}

using System;

namespace Domain.DTOs.OrderItems;

public class OrderItemGetDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }

}

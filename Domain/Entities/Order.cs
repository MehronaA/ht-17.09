using System;

namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
}

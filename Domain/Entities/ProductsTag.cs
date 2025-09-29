using System;

namespace Domain.Entities;

public class ProductsTag
{
    public int ProductId { get; set; }
    public int TagId { get; set; }

    public Product Product { get; set; } = new();
    public Tag Tag { get; set; } = new();
}

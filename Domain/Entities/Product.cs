using System;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int CategoryId { get; set; }


    public Category Category { get; set; } = new Category();
    public List<ProductsTag> ProductTags { get; set; } = new();
    public List<OrderItem> OrderItems  { get; set; } = [];
    
}

using System;

namespace Domain.DTOs.ProductTags;

public class ProductTagCreateDto
{
    public int ProductId { get; set; }
    public int TagId { get; set; }
}

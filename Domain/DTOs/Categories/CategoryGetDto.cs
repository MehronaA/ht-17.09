using System;

namespace Domain.DTOs.Categories;

public class CategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

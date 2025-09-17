using System;

namespace Domain.DTOs.Categories;

public class CategoryCreateDto
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}

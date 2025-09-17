using System;

namespace Domain.DTOs.Categories;

public class CategoryUpdateDto
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}

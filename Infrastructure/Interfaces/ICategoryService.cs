using System;
using Domain.DTOs.Categories;
using Infrastructure.APIResponce;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<Responce<IEnumerable<CategoryGetDto>>> GetActive();
    Task<Responce<string>> CreateItem(CategoryCreateDto dto);
    Task<Responce<string>> UpdateItem(int id, CategoryUpdateDto dto);
    Task<Responce<string>> DeleteItem(int id);
}

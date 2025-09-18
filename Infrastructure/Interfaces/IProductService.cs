using System;
using Domain.DTOs.Products;
using Infrastructure.APIResponce;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<Responce<IEnumerable<ProductGetDto>>> GetItems();
    Task<Responce<string>> CreateItem(ProductCreateDto dto);
    Task<Responce<string>> UpdateItem(int id, ProductUpdateDto dto);
    Task<Responce<string>> DeleteItem(int id);
}

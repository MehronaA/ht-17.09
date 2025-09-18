using System;
using System.Diagnostics;
using Domain.DTOs.Products;
using Domain.Entities;
using Infrastructure.APIResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly DataContext _context;

    public ProductService(DataContext context)
    {
        _context = context;
    }
    public async Task<Responce<string>> CreateItem(ProductCreateDto dto)
    {
        //Name
        if (string.IsNullOrWhiteSpace(dto.Name)) return Responce<string>.Fail(400, "Name is required");
        //Price
        if (dto.Price <= 0) return Responce<string>.Fail(400, "Price cannot be negative or 0");
        //stock
        if (dto.Stock < 0) return Responce<string>.Fail(400, "Stock cannot be negative ");
        //categoryid
        if (dto.CategoryId == 0) return Responce<string>.Fail(400, "Category is required");

        var newProduct = new Product()
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            IsActive = dto.IsActive,
            CategoryId = dto.CategoryId
        };

        var create = await _context.Products.AddAsync(newProduct);
        var result = await _context.SaveChangesAsync();
        return result == 0
                ? Responce<string>.Fail(500, "Internal Server Error")
                : Responce<string>.Created("Customer successfully created");   

    }

    public async Task<Responce<string>> DeleteItem(int id)
    {
        var exist = await _context.Products.FirstOrDefaultAsync(p => p.UserId == id);
        if (exist == null) return Responce<string>.Fail(404, "Product to delete not found");

        var delete = _context.Products.Remove(exist);
        var result = await _context.SaveChangesAsync();
        return result == 0
                ? Responce<string>.Fail(500, "Not deleted")
                : Responce<string>.Created("Deleted successfuly");
    }

    public async Task<Responce<IEnumerable<ProductGetDto>>> GetItems()
    {
        var items = await _context.Products.Select(p => new ProductGetDto()
        {
            Name = p.Name,
            Price = p.Price,
            Stock = p.Stock,
            IsActive = p.IsActive,
            CategoryId=p.CategoryId
        }).ToListAsync();
        return Responce<IEnumerable<ProductGetDto>>.Ok(items);
    }

    public Task<Responce<string>> UpdateItem(int id, ProductUpdateDto dto)
    {
        
    }
}

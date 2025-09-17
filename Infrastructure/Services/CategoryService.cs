using System;
using Domain.DTOs.Categories;
using Domain.Entities;
using Infrastructure.APIResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }
    public async Task<Responce<string>> CreateItem(CategoryCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return Responce<string>.Fail(409,"Name is required");
        
        if (dto.Name.Length > 255)return Responce<string>.Fail(409,"Category name is too long.");
          
        
        var exists = await _context.Categories
            .AnyAsync(c=> c.Name.ToLower() == dto.Name.ToLower().Trim());
            
        if (!exists) return Responce<string>.Fail(409, $"Category '{dto.Name}' already exists.");

        
        var newCategory = new Category()
        {
            Name = dto.Name.Trim(),
            IsActive = dto.IsActive
        };
        
        await _context.Categories.AddAsync(newCategory);
        var result=await _context.SaveChangesAsync();
        
        return result == 0
                ? Responce<string>.Fail(500, "Something goes wrong")
                : Responce<string>.Created("Created successfuly");
    }

    public async Task<Responce<string>> DeleteItem(int id)
    {
        var category =await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return Responce<string>.Fail(404, "Category with your id doesn exist");
        var items = _context.Categories.Remove(category);
        var result = await _context.SaveChangesAsync();
        return result == 0
                ? Responce<string>.Fail(500, "Not deleted")
                : Responce<string>.Created("Deleted successfuly");
    }

    public async  Task<Responce<IEnumerable<CategoryGetDto>>> GetActive()
    {
        var items = await _context.Categories
            .Select(c => new CategoryGetDto()
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive
            }).ToListAsync();

        return Responce<IEnumerable<CategoryGetDto>>.Ok(items);
    }

    public async Task<Responce<string>> UpdateItem(int id, CategoryUpdateDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)Responce<string>.Fail(404,  "Category not found.");
     
        
        if (string.IsNullOrWhiteSpace(dto.Name)) return Responce<string>.Fail(409,"Name is required");
        
        if (dto.Name.Length > 255)return Responce<string>.Fail(409,"Category name is too long.");

        
        var exists = await _context.Categories
            .AnyAsync(c=> c.Name.ToLower() == dto.Name.ToLower().Trim());
        if (!exists) return Responce<string>.Fail(409, $"Category '{dto.Name}' already exists.");
       
        
        bool noChanges = category.Name.ToLower() == dto.Name.ToLower().Trim() &&
                         category.IsActive == dto.IsActive; 
        if (noChanges) return Responce<string>.Fail(400, "No changes were made.");
        
        category.Name = dto.Name.Trim();
        category.IsActive = dto.IsActive;
        var result =await _context.SaveChangesAsync();
        
        return result == 0
                ? Responce<string>.Fail(500, "Not updated")
                : Responce<string>.Created("Updated successfuly");
    }
}

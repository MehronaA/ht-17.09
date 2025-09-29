using System;
using System.ComponentModel.DataAnnotations;
using Domain.DTOs.Tags;
using Domain.Entities;
using Infrastructure.APIResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TagService : ITagService
{
    private readonly DataContext _context;
    public TagService(DataContext context)
    {
        _context = context;   
    }
    public async Task<Responce<string>> CreateItem(TagCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return Responce<string>.Fail(409, "Tag name is required");
        if (dto.Name.Trim().Length > 30) return Responce<string>.Fail(409, "Tag name should be less than 30 characters");

        var exist = await _context.Tags.FindAsync(dto.Name);

        if (exist != null) return Responce<string>.Fail(400, "Tag already exist");

        var newTag = new Tag() { Name = dto.Name };

        await _context.Tags.AddAsync(newTag);
        var result = await _context.SaveChangesAsync();

        return result == 0
                ? Responce<string>.Fail(500, "Internal Server Error")
                : Responce<string>.Created("Customer successfully created");

    }

    public async Task<Responce<string>> DeleteItem(int id)
    {
        var exist = await _context.Tags.FindAsync(id);
        if (exist == null) return Responce<string>.Fail(404, "Tag to delete not found");

        _context.Remove(exist);
        var result = await _context.SaveChangesAsync();
        return result == 0
                ? Responce<string>.Fail(500, "Not deleted")
                : Responce<string>.Created("Deleted successfuly");
    }

    public async Task<Responce<IEnumerable<TagGetDto>>> GetItems()
    {
        var items = await _context.Tags.Select(t => new TagGetDto() { Name = t.Name }).ToListAsync();
        return Responce<IEnumerable<TagGetDto>>.Ok(items);
    }

    public async Task<Responce<string>> UpdateItem(int id, TagUpdateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return Responce<string>.Fail(409, "Tag name is required");
        if (dto.Name.Trim().Length > 30) return Responce<string>.Fail(409, "Tag name should be less than 30 characters");

        var exist = await _context.Tags.FindAsync(id);

        if (exist == null) return Responce<string>.Fail(404, "Tag to update not found");

        exist.Name = dto.Name;
        var result = await _context.SaveChangesAsync();

        return result == 0
                ? Responce<string>.Fail(500, "Not updated")
                : Responce<string>.Created("Updated successfuly");
    }
}

using System;
using Domain.DTOs.Users;
using Domain.Entities;
using Infrastructure.APIResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly DataContext _context;
    public UserService(DataContext context)
    {
        _context = context;
    }
    public async Task<Responce<string>> CreateItem(UserCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return Responce<string>.Fail(409, "Name is required");
        if (string.IsNullOrWhiteSpace(dto.PhoneNumber)) return Responce<string>.Fail(409, "Phone number is required");
        if (string.IsNullOrWhiteSpace(dto.Address)) return Responce<string>.Fail(409, "Address is required");

        var exist = await _context.Users.FirstOrDefaultAsync(u => u.Name == dto.Name && u.PhoneNumber == dto.PhoneNumber);

        if (exist != null) return Responce<string>.Fail(400, "User already exist");

        var newUser = new User()
        {
            Name = dto.Name,
            PhoneNumber = dto.PhoneNumber
        };
        await _context.Users.AddAsync(newUser);

        var newProfile = new Profile()
        {
            Address = dto.Address,
            UserId = newUser.Id
        };
        await _context.Profiles.AddAsync(newProfile);

        var result = await _context.SaveChangesAsync();
    }

    public async Task<Responce<string>> DeleteItem(int id)
    {
        var exist = await _context.Users.FindAsync(id);

        if (exist == null) return Responce<string>.Fail(400, "User ti delete not found");

        _context.Users.Remove(exist);
        var result = await _context.SaveChangesAsync();
    }

    public async Task<Responce<IEnumerable<UserGetDto>>> GetItems()
    {
        var items = await _context.Users.Select(u => new UserGetDto()
        {
            Id = u.Id,
            Name = u.Name,
            PhoneNumber = u.PhoneNumber,


        }).ToListAsync();
        return Responce<IEnumerable<UserGetDto>>.Ok(items);
    }

    public Task<Responce<string>> UpdateItem(int id, UserUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}

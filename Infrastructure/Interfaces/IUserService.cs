using System;
using Domain.DTOs.Users;
using Infrastructure.APIResponce;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Responce<IEnumerable<UserGetDto>>> GetItems();
    Task<Responce<string>> CreateItem(UserCreateDto dto);
    Task<Responce<string>> UpdateItem(int id, UserUpdateDto dto);
    Task<Responce<string>> DeleteItem(int id);
}

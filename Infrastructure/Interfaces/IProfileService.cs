using System;
using Domain.DTOs.Profiles;
using Infrastructure.APIResponce;

namespace Infrastructure.Interfaces;

public interface IProfileService
{
    Task<Responce<IEnumerable<ProfileGetDto>>> GetItems();
    Task<Responce<string>> CreateItem(ProfileCreateDto dto);
    Task<Responce<string>> UpdateItem(int id, ProfileUpdateDto dto);
    Task<Responce<string>> DeleteItem(int id);
}

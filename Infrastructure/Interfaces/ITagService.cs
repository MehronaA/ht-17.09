using System;
using Domain.DTOs.Tags;
using Infrastructure.APIResponce;

namespace Infrastructure.Interfaces;

public interface ITagService
{
    Task<Responce<IEnumerable<TagGetDto>>> GetItems();
    Task<Responce<string>> CreateItem(TagCreateDto dto);
    Task<Responce<string>> UpdateItem(int id, TagUpdateDto dto);
    Task<Responce<string>> DeleteItem(int id);
}

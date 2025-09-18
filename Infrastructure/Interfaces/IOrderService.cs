using System;
using Domain.DTOs.Orders;
using Infrastructure.APIResponce;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
    Task<Responce<IEnumerable<OrderGetDto>>> GetItems();
    Task<Responce<string>> CreateItem(OrderCreateDto dto);
    Task<Responce<string>> UpdateItem(int id, OrderUpdateDto dto);
    Task<Responce<string>> DeleteItem(int id);
}

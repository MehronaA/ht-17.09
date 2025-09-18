using System;
using Domain.DTOs.Orders;
using Domain.Entities;
using Infrastructure.APIResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }
    public async Task<Responce<string>> CreateItem(OrderCreateDto dto)
    {
        if (dto.UserId == 0) return Responce<string>.Fail(409, "User Id is required");
        var exist = await _context.Orders.AnyAsync(o => o.UserId == dto.UserId);
        if (!exist) return Responce<string>.Fail(409, $"Order with this User Id:{dto.UserId} already exist");

        var newOrder = new Order()
        {
            UserId = dto.UserId
        };
        await _context.Orders.AddAsync(newOrder);
        var result = await _context.SaveChangesAsync();
        return result == 0
                ? Responce<string>.Fail(500, "Internal Server Error")
                : Responce<string>.Created("Customer successfully created");   
    }

    public async Task<Responce<string>> DeleteItem(int id)
    {
        var exist = await _context.Orders.FirstOrDefaultAsync(o => o.UserId == id);
        if (exist == null) return Responce<string>.Fail(404, "Order to delete not found");

        var delete = _context.Orders.Remove(exist);
        var result = await _context.SaveChangesAsync();
        return result == 0
                ? Responce<string>.Fail(500, "Not deleted")
                : Responce<string>.Created("Deleted successfuly");
    }

    public async Task<Responce<IEnumerable<OrderGetDto>>> GetItems()
    {
        var items = await _context.Orders.Select(o => new OrderGetDto() { UserId = o.UserId }).ToListAsync();
        return Responce<IEnumerable<OrderGetDto>>.Ok(items);
    }

    public async Task<Responce<string>> UpdateItem(int id, OrderUpdateDto dto)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return Responce<string>.Fail(404, "Order to update not found");

        var noChanges = order.UserId == dto.UserId;
        if (noChanges) return Responce<string>.Fail(400, "No changes were made");

        order.UserId = dto.UserId;
        var result = await _context.SaveChangesAsync();
        return result == 0
                ? Responce<string>.Fail(500, "Not updated")
                : Responce<string>.Created("Updated successfuly");


    }
}

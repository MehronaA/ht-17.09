using System;
using Domain.Entities;

namespace Domain.DTOs.Users;

public class UserGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public Profile Profiles { get; set; } = new();
}

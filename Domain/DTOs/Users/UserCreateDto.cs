using System;

namespace Domain.DTOs.Users;

public class UserCreateDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    
}

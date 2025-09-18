using System;

namespace Domain.DTOs.Users;

public class UserGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}

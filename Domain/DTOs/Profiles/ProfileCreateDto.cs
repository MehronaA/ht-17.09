using System;

namespace Domain.DTOs.Profiles;

public class ProfileCreateDto
{
    public int UserId { get; set; }
    public string Address { get; set; }
}

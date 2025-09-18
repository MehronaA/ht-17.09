using System;

namespace Domain.DTOs.Profiles;

public class ProfileUpdateDto
{
    public int UserId { get; set; }
    public string Address { get; set; }
}

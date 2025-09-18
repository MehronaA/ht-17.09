using System;

namespace Domain.DTOs.Profiles;

public class ProfileGetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Address { get; set; }
}

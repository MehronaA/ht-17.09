using System;

namespace Domain.Entities;

public class Profile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Address { get; set; }


    public User User { get; set; }
}

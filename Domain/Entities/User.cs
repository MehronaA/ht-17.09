using System;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public List<Order> Orders { get; set; }
    public Profile Profile { get; set; }
}

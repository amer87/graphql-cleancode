using System;
using System.Collections.Generic;

namespace Com.Domain.Entities;

public class Shop : BaseEntity
{
    public Shop()
    {
        Items = new List<Item>();
        Users = new List<User>();
    }

    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsApproved { get; set; } = false;
    public ICollection<Item> Items { get; private set; }
    public ICollection<User> Users { get; private set; }
    public Guid? OwnerId { get; set; }
    public User? Owner { get; set; }
    public ICollection<CMFile>  Files { get; set; }
}

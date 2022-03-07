using System;
using System.Collections.Generic;
using static Com.Domain.Constants.Constants;

namespace Com.Domain.Entities;

public class Item : BaseEntity
{
    public Item()
    {
        Files = new List<CMFile>();
    }

    public string Code { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? SystemReference { get; set; }
    public double Price { get; set; }
    public int AvailableQuantity { get; set; }
    public short Status { get; set; }
    public Guid ShopId { get; set; }
    public Shop Shop { get; set; }
    public short Type { get; set; }
    public User Owner { get; set; }
    public Guid? UserId { get; set; }
    public Guid EntryId { get; set; }
    public int LineNumber { get; set; } = Entry.HEADERLINE;
    public virtual List<CMFile> Files { get; set; }
}

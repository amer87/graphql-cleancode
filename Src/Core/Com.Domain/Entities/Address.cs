using System;

namespace Com.Domain.Entities;

public class Address : BaseEntity
{
    public string? Title { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public Guid BelongsTo { get; set; }
    public short Type { get; set; }
}
using System;

namespace Com.Domain.Entities;

public class CMFile : BaseEntity
{
    public string Path { get; set; } = string.Empty;
    public string FileExtention { get; set; }
    public short FileUsage { get; set; }
    public Guid OwnerId { get; set; }
    public string Owner { get; set; }
}

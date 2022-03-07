using System;

namespace Com.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public Guid Creator { get; set; }
    public DateTime AddedDate { get; set; }
    public Guid Modifier { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }

}
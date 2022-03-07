using System;
using System.Linq;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Items.Tools;

public class ItemNumbersTool
{
    public static int GetLineNumber(IRepository<Item> repo, Guid entryId)
    {
        return repo.Find(x => x.EntryId.Equals(entryId)).Count();
    }
}

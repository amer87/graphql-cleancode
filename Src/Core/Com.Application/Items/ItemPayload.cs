using Com.Application.Common.GraphQL;
using Com.Domain.Entities;

namespace Com.Application.Items;

public class ItemPayload : Payload<Item>
{
    public ItemPayload(Item item) : base(item)  { }

    public ItemPayload(UserError error)
        : base(new[] { error })
    {
    }

    public ItemPayload(string message, string code) : base(message, code) { }
}

using Com.Application.Common.GraphQL;
using Com.Domain.Entities;

namespace Com.Application.Shops;

public class ShopPayload : Payload<Shop>
{
    public ShopPayload(Shop s) : base(s)  { }

    public ShopPayload(UserError error) : base(new[] { error }) {}

    public ShopPayload(string message, string code): base(message, code) { }
}

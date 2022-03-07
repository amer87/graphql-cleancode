using Com.Application.Common.GraphQL;
using Com.Domain.Entities;

namespace Com.Application.Addresses;

public class AddressPayload : Payload<Address>
{
    public AddressPayload(Address s) : base(s)  { }

    public AddressPayload(string message, string code) : base(message, code) { }
}

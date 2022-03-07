using HotChocolate;
using Newtonsoft.Json;
using Com.Application.Common.Constants;
using Com.Application.Common.GraphQL;
using Com.Domain.Entities;

namespace Com.Web.Api.Common;

class GenericPayLoad : Payload<BaseEntity>
{
    public GenericPayLoad(UserError error) : base(new[] { error }) { }
}

internal class GraphQLExceptionHandlerFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        Payload<BaseEntity> pl = new GenericPayLoad(new UserError(error.Message, Constants.StringResults.NOK));
        return new Error(JsonConvert.SerializeObject(pl), Constants.StringResults.NOK);
    }
}

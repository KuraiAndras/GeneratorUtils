using GeneratorUtils.Samples.Api.Responses.Items;
using MediatR;

namespace GeneratorUtils.Samples.Api.Requests.Items
{
    public sealed class GetAllItems : IRequest<ItemsResponse>
    {
    }
}

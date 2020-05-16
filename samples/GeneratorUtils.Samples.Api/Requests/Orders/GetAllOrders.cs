using MediatR;

namespace GeneratorUtils.Samples.Api.Requests.Orders
{
    public sealed class GetAllOrders : IRequest<OrdersResponse>
    {
    }
}
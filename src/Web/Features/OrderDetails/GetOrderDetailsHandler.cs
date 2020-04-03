using MediatR;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Specifications;
using Nethereum.eShop.Web.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Features.OrderDetails
{
    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetails, OrderViewModel>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderDetailsHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderViewModel> Handle(GetOrderDetails request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithItemsAsync(request.OrderId);

            if (order == null)
            {
                return null;
            }

            return new OrderViewModel
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Quantity
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShipTo,
                Total = order.Total()
            };
        }
    }
}

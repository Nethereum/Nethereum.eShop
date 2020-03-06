using AutoMapper;
using MediatR;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using Nethereum.eShop.ApplicationCore.Specifications;
using Nethereum.eShop.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Features.MyOrders
{
    public class GetMyOrdersHandler : IRequestHandler<GetMyOrders, IEnumerable<OrderExcerptViewModel>>
    {
        private readonly static Mapper _mapper;
        static GetMyOrdersHandler()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderExcerptProfile>());
            _mapper = new Mapper(config);
        }

        private readonly IOrderQueries _orderQueries;

        public GetMyOrdersHandler(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public async Task<IEnumerable<OrderExcerptViewModel>> Handle(GetMyOrders request, CancellationToken cancellationToken)
        {
            var specification = new CustomerOrdersWithItemsSpecification(request.UserName);
            var orders = await _orderQueries.GetByBuyerIdAsync(request.UserName, fetch: 100);

            return orders.Data.Select(o => _mapper.Map<OrderExcerptViewModel>(o));
        }
    }

    public class OrderExcerptProfile : Profile
    {
        public OrderExcerptProfile()
        {
            this.CreateMap<OrderExcerpt, OrderExcerptViewModel>();
        }
    }
}

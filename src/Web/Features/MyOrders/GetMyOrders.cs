using MediatR;
using Nethereum.eShop.Web.ViewModels;
using System.Collections.Generic;

namespace Nethereum.eShop.Web.Features.MyOrders
{
    public class GetMyOrders : IRequest<IEnumerable<OrderExcerptViewModel>>
    {
        public string UserName { get; set; }

        public GetMyOrders(string userName)
        {
            UserName = userName;
        }
    }
}

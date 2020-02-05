using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;

namespace Nethereum.eShop.ApplicationCore.Specifications
{
    public class CustomerOrdersWithItemsSpecification : BaseSpecification<Order>
    {
        public CustomerOrdersWithItemsSpecification(string buyerId)
            : base(o => o.BuyerAddress == buyerId)
        {
            AddInclude(o => o.OrderItems);
            AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}");
        }
    }
}

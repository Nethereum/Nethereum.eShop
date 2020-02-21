using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;

namespace Nethereum.eShop.ApplicationCore.Specifications
{
    public class CustomerQuotesWithItemsSpecification : BaseSpecification<Quote>
    {
        public CustomerQuotesWithItemsSpecification(string buyerId)
            : base(o => o.BuyerId == buyerId)
        {
            AddInclude(o => o.QuoteItems);
            AddInclude($"{nameof(Quote.QuoteItems)}.{nameof(QuoteItem.ItemOrdered)}");
        }
    }
}

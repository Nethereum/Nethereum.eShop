using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;

namespace Nethereum.eShop.ApplicationCore.Specifications
{
    public sealed class QuoteWithItemsSpecification : BaseSpecification<Quote>
    {
        public QuoteWithItemsSpecification(int quoteId)
            : base(b => b.Id == quoteId)
        {
            AddInclude(b => b.QuoteItems);
        }
    }
}

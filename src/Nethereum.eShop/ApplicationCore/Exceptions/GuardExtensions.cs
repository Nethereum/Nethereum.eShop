using Nethereum.eShop.ApplicationCore.Exceptions;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;

namespace Ardalis.GuardClauses
{
    public static class BasketGuards
    {
        public static void NullBasket(this IGuardClause guardClause, int basketId, Basket basket)
        {
            if (basket == null)
                throw new BasketNotFoundException(basketId);
        }
    }

    public static class QuoteGuards
    {
        public static void NullQuote(this IGuardClause guardClause, int quoteId, Quote quote)
        {
            if (quote == null)
                throw new QuoteNotFoundException(quoteId);
        }
    }
}
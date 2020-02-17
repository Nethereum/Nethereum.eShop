using MediatR;
using Nethereum.eShop.Web.ViewModels;

namespace Nethereum.eShop.Web.Features.QuoteDetails
{
    public class GetQuoteDetails : IRequest<QuoteViewModel>
    {
        public string UserName { get; set; }
        public int QuoteId { get; set; }

        public GetQuoteDetails(string userName, int quoteId)
        {
            UserName = userName;
            QuoteId = quoteId;
        }
    }
}

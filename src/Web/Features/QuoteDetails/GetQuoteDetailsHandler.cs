using MediatR;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Specifications;
using Nethereum.eShop.Web.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Features.QuoteDetails
{
    public class GetQuoteDetailsHandler : IRequestHandler<GetQuoteDetails, QuoteViewModel>
    {
        private readonly IQuoteRepository _quoteRepository;

        public GetQuoteDetailsHandler(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task<QuoteViewModel> Handle(GetQuoteDetails request, CancellationToken cancellationToken)
        {
            var customerQuotes = await _quoteRepository.ListAsync(new CustomerQuotesWithItemsSpecification(request.UserName));
            var quote = customerQuotes.FirstOrDefault(o => o.Id == request.QuoteId);

            if (quote == null)
            {
                return null;
            }

            return new QuoteViewModel
            {
                QuoteDate = quote.Date,
                QuoteItems = quote.QuoteItems.Select(oi => new QuoteItemViewModel
                {
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Quantity
                }).ToList(),
                QuoteId = quote.Id,
                Status = quote.Status.ToString(),
                TransactionHash = quote.TransactionHash,
                ShipTo = quote.ShipTo,
                BillTo = quote.BillTo,
                Total = quote.Total()
            };
        }
    }
}

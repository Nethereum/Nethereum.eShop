using MediatR;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Specifications;
using Nethereum.eShop.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Features.MyQuotes
{
    public class GetMyQuotesHandler : IRequestHandler<GetMyQuotes, IEnumerable<QuoteViewModel>>
    {
        private readonly IQuoteRepository _quoteRepository;

        public GetMyQuotesHandler(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task<IEnumerable<QuoteViewModel>> Handle(GetMyQuotes request, CancellationToken cancellationToken)
        {
            var specification = new CustomerQuotesWithItemsSpecification(request.UserName);
            var orders = await _quoteRepository.ListAsync(specification);

            return orders.Select(o => new QuoteViewModel
            {
                QuoteDate = o.Date,
                QuoteItems = o.QuoteItems?.Select(oi => new QuoteItemViewModel()
                {
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Quantity
                }).ToList(),
                QuoteId = o.Id,
                Status = o.Status.ToString(),
                TransactionHash = o.TransactionHash,
                ShipTo = o.ShipTo,
                BillTo = o.BillTo,
                Total = o.Total()
            });
        }
    }
}

using AutoMapper;
using MediatR;
using Nethereum.eShop.ApplicationCore.Queries;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using Nethereum.eShop.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Features.MyQuotes
{

    public class GetMyQuotesHandler : IRequestHandler<GetMyQuotes, IEnumerable<QuoteExcerptViewModel>>
    {
        private readonly static Mapper _mapper;
        static GetMyQuotesHandler()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<QuoteExcerptProfile>());
            _mapper = new Mapper(config);
        }

        private readonly IQuoteQueries _quoteQueries;

        public GetMyQuotesHandler(IQuoteQueries quoteRepository)
        {
            _quoteQueries = quoteRepository;
        }

        public async Task<IEnumerable<QuoteExcerptViewModel>> Handle(GetMyQuotes request, CancellationToken cancellationToken)
        {
            var quotes = await _quoteQueries.GetByBuyerIdAsync(request.UserName, new PaginationArgs { Fetch = 100, SortDescending = true });
            return quotes.Data.Select(excerpt => _mapper.Map<QuoteExcerptViewModel>(excerpt));
        }
    }

    public class QuoteExcerptProfile : Profile
    {
        public QuoteExcerptProfile()
        {
            this.CreateMap<QuoteExcerpt, QuoteExcerptViewModel>();
        }
    }
}

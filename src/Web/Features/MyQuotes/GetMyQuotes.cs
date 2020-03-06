using MediatR;
using Nethereum.eShop.Web.ViewModels;
using System.Collections.Generic;

namespace Nethereum.eShop.Web.Features.MyQuotes
{
    public class GetMyQuotes : IRequest<IEnumerable<QuoteExcerptViewModel>>
    {
        public string UserName { get; set; }

        public GetMyQuotes(string userName)
        {
            UserName = userName;
        }
    }
}

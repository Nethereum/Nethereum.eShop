using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nethereum.eShop.Web.Features.MyQuotes;
using Nethereum.eShop.Web.Features.QuoteDetails;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize] // Controllers that mainly require Authorization still use Controller/View; other pages use Pages
    [Route("[controller]/[action]")]
    public class QuoteController : Controller
    {
        private readonly IMediator _mediator;

        public QuoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> MyQuotes()
        {
            var viewModel = await _mediator.Send(new GetMyQuotes(User.Identity.Name));

            return View(viewModel);
        }

        [HttpGet("{quoteId}")]
        public async Task<IActionResult> Detail(int quoteId)
        {
            var viewModel = await _mediator.Send(new GetQuoteDetails(User.Identity.Name, quoteId));

            if (viewModel == null)
            {
                return BadRequest("No such quote found for this user.");
            }

            return View(viewModel);
        }
    }
}

using MediatR;
using Nethereum.eShop.ApplicationCore.Entities;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, CatalogContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}

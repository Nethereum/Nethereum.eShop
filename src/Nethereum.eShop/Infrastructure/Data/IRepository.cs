namespace Nethereum.eShop.Infrastructure.Data
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

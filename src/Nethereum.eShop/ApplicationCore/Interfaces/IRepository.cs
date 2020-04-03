namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

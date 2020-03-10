using System.Reflection;

namespace Nethereum.eShop.Infrastructure.Data
{
    public interface IModelBuilderAssemblyHandler<T>
    {
        Assembly GetModelBuilderAssembly();
    }
}

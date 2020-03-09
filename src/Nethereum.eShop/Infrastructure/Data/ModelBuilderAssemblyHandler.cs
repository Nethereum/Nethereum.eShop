using System.Reflection;

namespace Nethereum.eShop.Infrastructure.Data
{
    public class ModelBuilderAssemblyHandler<T> : IModelBuilderAssemblyHandler<T>
    {
        private readonly Assembly _assembly;

        public ModelBuilderAssemblyHandler(Assembly assembly)
        {
            _assembly = assembly;
        }
        public Assembly GetModelBuilderAssembly() => _assembly;
    }
}

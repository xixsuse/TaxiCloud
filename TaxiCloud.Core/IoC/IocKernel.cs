using Ninject;
using Ninject.Modules;

namespace TaxiCloud.Core.IoC
{
    public static class IocKernel
    {
        private static StandardKernel _kernel;

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static T Get<T>(string name)
        {
            return _kernel.Get<T>(name);
        }

        public static void Inject(object instance)
        {
            _kernel.Inject(instance);
        }

        public static void Initialize(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(modules);
        }
    }
}
using System;
using Autofac;

namespace LockerClient
{
    /// <summary>
    /// Autofac extension to install Locker as DI module
    /// </summary>
    public static class AutofacExtension
    {
        /// <summary>
        /// Register Locker in autofac DI container
        /// </summary>
        /// <param name="builder">Autofac container</param>
        public static void RegisterLockerExtension(this ContainerBuilder builder)
        {
            _ = builder.RegisterType<Locker>().As<ILocker>();
        }
    }
}

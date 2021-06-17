using ColoredLive.BL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ColoredLive.Service.Core.Utils
{
    public static class BlAutoImplementor
    {
        
        public static void Implement<T>(IServiceCollection serviceProvider) {
            
            
            var assemblyFullName = typeof(T).Assembly.FullName;
            Assembly.Load(assemblyFullName);

            var requiredAssembly = AppDomain.CurrentDomain.GetAssemblies().First(t => t.FullName == assemblyFullName);
            var assemblyTypes = requiredAssembly.GetTypes().Where(t => t.GetCustomAttribute<BuisnessAttribute>() != null);

            foreach (var blType in assemblyTypes)
            {
                foreach (var @interface in blType.GetInterfaces() )
                {
                    serviceProvider.AddScoped(@interface, blType);
                }
            }
        }
    }
}

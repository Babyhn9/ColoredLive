using ColoredLive.BL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ColoredLive.MainService.Utils
{
    internal static class BlAutoImplementor
    {
        public static void Implement(IServiceCollection serviceProvider)
        {
            new BuisnessAttribute(); // заставляет приложение подгрузить сборку ColoredLive.BL, т.к до этого момента она ни где не используется
            var requiredAssembly = AppDomain.CurrentDomain.GetAssemblies().First(t => t.GetName().Name == "ColoredLive.BL");
            var assemblyTypes = requiredAssembly.GetTypes().Where(t => t.GetCustomAttribute<BuisnessAttribute>() != null);

            foreach (var blType in assemblyTypes)
            {
                //new multi interface implementation
                foreach (var @interface in blType.GetInterfaces() )
                {
                    serviceProvider.AddScoped(@interface, blType);
                }
                //old single interface implementation: serviceProvider.AddScoped(blType.GetInterfaces().First() ?? throw new NotImplementedException(), blType);
            }


        }

    }
}

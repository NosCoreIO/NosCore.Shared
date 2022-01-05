//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NosCore.Shared.I18N;

public static class IServiceCollectionExtension
{
    private static void LoadReferencedAssembly(Assembly assembly)
    {
        foreach (var name in assembly.GetReferencedAssemblies().Where(x=>x.Name?.Contains(nameof(NosCore)) ?? false))
        {
            if (AppDomain.CurrentDomain.GetAssemblies().All(a => a.FullName != name.FullName))
            {
                LoadReferencedAssembly(Assembly.Load(name));
            }
        }
    }
    public static IServiceCollection AddI18NLogs(this IServiceCollection services)
    {
        LoadReferencedAssembly(Assembly.GetExecutingAssembly());
        var types = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName?.Contains(nameof(NosCore)) ?? false)
            .SelectMany(x => x.GetTypes())
            .Where(t => t.IsEnum && t.IsPublic && t.Name == "LogLanguageKey")
            .ToList();

        services.AddLocalization();
        foreach (var type in types)
        {
            var res = type.Assembly.GetTypes().FirstOrDefault(t => t.Name == "LocalizedResources");
            if (res == null)
            {
                continue;
            }
            var generic = typeof(LogLanguageLocalizer<,>);
            var genericInterface = typeof(ILogLanguageLocalizer<>);
            services.AddTransient(genericInterface.MakeGenericType(type), generic.MakeGenericType(type, res));
        }

        return services;
    }
}
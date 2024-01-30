using System.Reflection;
using ProcessBot.Interfaces;

namespace ProcessBot;
using Microsoft.Extensions.DependencyInjection;

public static class ProcessBotServiceCollectionExtensions
{
    public static IServiceCollection AddProcessBot(
        this IServiceCollection services, 
        Action<ProcessBotOptions> setupAction, 
        Type actFactoryType,
        params Assembly[] actAssemblies)
    {
        services.AddOptions();
        services.Configure(setupAction);
        
        services.AddSingleton<ProcessBot>();
        services.AddTransient(typeof(IProcessActFactory), actFactoryType);
        AddTasksImplementations(services, actAssemblies);
        services.AddHostedService<ProcessBotHostedService>();

        return services;
    }
    
    private static void AddTasksImplementations(IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
    {
        List<TypeInfo> concretions = assembliesToScan
            .SelectMany(a => a.DefinedTypes)
            .Where(type => type.FindInterfacesThatClose(typeof(IProcessAct)).Any())
            .ToList();

        foreach (TypeInfo type in concretions)
        {
            services.AddTransient(type);
        }
    }
    
    private static IEnumerable<Type> FindInterfacesThatClose(this Type pluggedType, Type templateType)
    {
        return FindInterfacesThatClosesCore(pluggedType, templateType).Distinct();
    }
    
    private static IEnumerable<Type> FindInterfacesThatClosesCore(Type pluggedType, Type templateType)
    {
        if (pluggedType == null) yield break;

        if (!pluggedType.IsConcrete()) yield break;

        if (templateType.GetTypeInfo().IsInterface)
        {
            foreach (
                var interfaceType in
                pluggedType.GetInterfaces()
                    .Where(type => (type == templateType)))
            {
                yield return interfaceType;
            }
        }
    }
    
    private static bool IsConcrete(this Type type)
    {
        return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
    }
}
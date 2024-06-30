using System.Reflection;

namespace estate_emporium.Utils
{
    public static class ServiceRegistrationUtils
    {
        public static void AddServicesFromNamespace(this IServiceCollection services, Assembly assembly, string namespacePrefix)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace != null && t.Namespace.StartsWith(namespacePrefix))
                .ToList();
            foreach (var type in types)
            {
                 services.AddScoped(type);
            }
        }
    }
}

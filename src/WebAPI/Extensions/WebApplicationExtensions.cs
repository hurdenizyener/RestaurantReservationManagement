using System.Reflection;
using WebAPI.Common;

namespace WebAPI.Extensions;

public static class WebApplicationExtensions
{

    public static WebApplication MapAllEndpointGroups(this WebApplication app)
    {
        var baseEndpointGroupType = typeof(BaseEndpointGroup);
        var assembly = Assembly.GetExecutingAssembly();
        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(baseEndpointGroupType) && !t.IsAbstract);

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is BaseEndpointGroup endpointGroup)
            {
                endpointGroup.Map(app);
            }
        }

        return app;
    }
}

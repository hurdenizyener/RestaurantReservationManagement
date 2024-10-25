using System.Reflection;

namespace WebAPI.Extensions;

public static class MethodInfoExtensions
{
    public static bool IsAnonymous(this MethodInfo method)
    {
        return method.Name.Contains('<') || method.Name.Contains('>');
    }
}

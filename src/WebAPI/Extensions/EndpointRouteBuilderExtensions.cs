namespace WebAPI.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoint(this IEndpointRouteBuilder builder, string httpMethod, Delegate handler, string pattern)
    {
        if (handler.Method.IsAnonymous())
            throw new ArgumentException("Anonim metodlar kullanılamaz. Lütfen metod adı belirtiniz.");

        builder.MapMethods(pattern, [httpMethod], handler)
               .WithName(handler.Method.Name);

        return builder;
    }
}

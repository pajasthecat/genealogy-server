using Microsoft.AspNetCore.Builder;

namespace Geneology.Api.Middleware
{
    public static class ErrorHandlingMiddlewareExtension
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
}
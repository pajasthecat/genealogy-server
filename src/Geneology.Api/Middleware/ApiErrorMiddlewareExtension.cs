using Microsoft.AspNetCore.Builder;

namespace Geneology.Api.Middleware
{
    public static class ApiErrorMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiErrorMiddleware>();
        }
    }
}
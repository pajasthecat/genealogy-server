using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Geneology.Api.Middleware
{
    //https://stackoverflow.com/questions/54104138/mediatr-fluent-validation-response-from-pipeline-behavior
    public class ApiErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            return exception switch
            {
                ValidationException validationException =>
                AddToContext(
                       context,
                       HttpStatusCode.BadRequest,
                       validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage })),
                _ => AddToContext(
                        context,
                        HttpStatusCode.InternalServerError,
                       new { isSuccess = false, error = exception.Message })

            };
        }

        private Task AddToContext(HttpContext context, HttpStatusCode code, object objecToSerialize)
        {
            var result = JsonConvert.SerializeObject(objecToSerialize);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
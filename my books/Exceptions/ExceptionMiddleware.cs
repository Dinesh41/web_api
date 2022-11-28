using my_books.Data.ViewModels;
using System.Net;

namespace my_books.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                 await _next(httpContext);
            }
            catch(Exception ex)
            {
                await handleException(httpContext,ex);
            }
        }
        public Task handleException(HttpContext httpContext,Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(new ErrorVM()
            {
                Code = httpContext.Response.StatusCode,
                Message = "Custom Exception Handling Middleware"
            }.ToString());
        }
    }
}

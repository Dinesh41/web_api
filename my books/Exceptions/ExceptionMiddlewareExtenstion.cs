using Microsoft.AspNetCore.Diagnostics;
using my_books.Data.ViewModels;
using System.Net;

namespace my_books.Exceptions
{
    public static class ExceptionMiddlewareExtenstion
    {
        //Extension method in IApplicationBuilder
        public static void ConfigureBuiltInExceptionHandler(this IApplicationBuilder app)
        {
            //Handling Exception in app level
            app.UseExceptionHandler(appError =>
            {
                //Runs below code when exception is captured
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    //Writing the response
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null) { 
                        await context.Response.WriteAsync(new ErrorVM()
                        {
                            Code = context.Response.StatusCode,
                            Message = "Built In Exception Handler"
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

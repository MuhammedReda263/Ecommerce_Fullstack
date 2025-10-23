
using Microsoft.Extensions.Caching.Memory;

namespace Ecom.API.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly IHostEnvironment _hostEnvironment;
     

        public GlobalExceptionMiddleware(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
           
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                var response = new
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Details = _hostEnvironment.IsDevelopment() ? ex.StackTrace?.ToString() : null
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }

        
    }

   

    }

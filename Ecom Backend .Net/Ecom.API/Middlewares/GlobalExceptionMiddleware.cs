
namespace Ecom.API.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
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

                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error",
                    Details = ex.Message
                });
            }
        }
    }
}

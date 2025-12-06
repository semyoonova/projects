
namespace AspLessons
{
    public class CheckHeaderMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Headers.Any(header => header.Key == "mysite"))
            {
                Console.WriteLine("Добро пожаловать, гость" );
            }
            else
            {
                Console.WriteLine("Мы вас не знаем" );
            }
            await next(context);
        }
    }
}

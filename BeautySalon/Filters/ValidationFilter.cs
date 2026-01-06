using FluentValidation;
using System.Net;

namespace BeautySalon.Filters
{
    public class ValidationFilter<T>: IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var argToValidate = context.GetArgument<T>(0);
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>( );

            if(validator is null)
            {
                return await next.Invoke(context);
            }

            var validationResult = await validator.ValidateAsync(argToValidate!);
            Console.WriteLine(validationResult);
            if(!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary( ),
                    statusCode: (int)HttpStatusCode.UnprocessableEntity);
            }

            return await next.Invoke(context);
        }
    }
}

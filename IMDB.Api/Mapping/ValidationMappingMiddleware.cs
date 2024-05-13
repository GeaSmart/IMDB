using Movies.Contracts.Responses;
using FluentValidation;

namespace IMDB.Api.Mapping
{
    public class ValidationMappingMiddleware
    {
        private readonly RequestDelegate next;

        public ValidationMappingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = ex.Errors.Select(x=>new ValidationResponse
                    {
                        PropertyName = x.PropertyName,
                        Message = x.ErrorMessage
                    })
                };
                await context.Response.WriteAsJsonAsync(validationFailureResponse);
            }            
        }
    }
}

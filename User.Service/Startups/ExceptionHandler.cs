using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Services.Shared.Domain.Exceptions;
using Services.Shared.Extensions;
using System.Threading.Tasks;

namespace User.Service.Startups
{
    public static class ExceptionHandler
    {
        public static async Task Handle(HttpContext context, IWebHostEnvironment env)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;

            if (env.IsDevelopment())
            {
                await context.Response.WriteAsJsonAsync(exception.FormatException());
                return;
            }

            if (exception.GetType().IsSubclassOf(typeof(UserException)))
            {
                await context.Response.WriteAsJsonAsync(exception.Message);
                return;
            }

            await context.Response.WriteAsJsonAsync("Internal Error");
        }
    }
}

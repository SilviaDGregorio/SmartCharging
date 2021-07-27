using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain;
using System;
using System.Collections.Generic;

namespace SmartCharging.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]

        public IActionResult ErrorLocalDevelopment(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return context.Error switch
            {
                KeyNotFoundException => CreateProblem(context, statusCode: StatusCodes.Status404NotFound),
                ConnectorsException => CreateProblem(context, statusCode: StatusCodes.Status409Conflict),
                _ => CreateProblem(context, null)
            };

        }



        [Route("/error")]
        public IActionResult Error() => Problem();

        private ObjectResult CreateProblem(IExceptionHandlerFeature context, int? statusCode)
        {
            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message,
                statusCode: statusCode ?? 500);
        }
    }
}

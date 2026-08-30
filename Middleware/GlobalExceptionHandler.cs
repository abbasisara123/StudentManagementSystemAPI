using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.DTOs.Common;

namespace StudentManagement.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new ErrorResponseDto
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "An expected error occurred."
            };

            await httpContext.Response.WriteAsJsonAsync(
                errorResponse,
                cancellationToken);

            return true;
        }
    }
}

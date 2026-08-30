using Microsoft.AspNetCore.Diagnostics;
using StudentManagement.API.DTOs.Common;
using System.Text.Json;

namespace StudentManagement.API.Middleware
{
    public class ExceptionHandlingMiddleware 
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next) 
        { 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorResponse = new ErrorResponseDto
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An unexpected error occurred."
                };

                var jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
        }

    }
}

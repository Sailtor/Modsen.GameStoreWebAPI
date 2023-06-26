using BLL.Exceptions;
using BLL.Infrastructure.Logger;
using DAL.Exceptions;
using DAL.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace API.Middleware.Exception_Handler
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception has been thrown : {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                DbUpdateException => (int)HttpStatusCode.BadRequest,
                SecurityTokenSignatureKeyNotFoundException => (int)HttpStatusCode.Unauthorized,
                ObjectDisposedException => 501,
                ModelValidationFailedException => (int)HttpStatusCode.BadRequest,
                DatabaseSaveFailedException => (int)HttpStatusCode.InternalServerError,
                EntityAlreadyExistsException => (int)HttpStatusCode.Conflict,
                WrongPasswordException => (int)HttpStatusCode.BadRequest,
                UserUnauthorizedException => (int)HttpStatusCode.Forbidden,
                InvalidRequestException => (int)HttpStatusCode.BadRequest,
                DatabaseNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            await context.Response.WriteAsync(new ErrorDetails()
            {
                Message = exception switch
                {
                    DbUpdateException => "Invalid database operation (are you trying to create existing resource?)",
                    SecurityTokenSignatureKeyNotFoundException => "Invalid authorization token",
                    ObjectDisposedException => "WTF IS OBJECT DISPOSED EXCEPTION",
                    ModelValidationFailedException => "Invalid model",
                    DatabaseSaveFailedException => "Database save changes process failed",
                    EntityAlreadyExistsException => "Entity with this id already exists",
                    WrongPasswordException => "Wrong user password",
                    UserUnauthorizedException => "Forbidden",
                    InvalidRequestException => "Invalid request",
                    DatabaseNotFoundException => "Object not found",
                    _ => "Internal server error"
                }
            }.ToString());
        }
    }
}

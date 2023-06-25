﻿using BLL.Exceptions;
using BLL.Infrastructure.Logger;
using DAL.Exceptions;
using DAL.Models;
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
                _logger.LogError($"Exception was thrown : {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = exception switch
                {
                    DatabaseSaveFailedException => (int)HttpStatusCode.InternalServerError,
                    EntityAlreadyExistsException => (int)HttpStatusCode.Conflict,
                    WrongPasswordException => (int)HttpStatusCode.BadRequest,
                    UserUnauthorizedException => (int)HttpStatusCode.Forbidden,
                    InvalidRequestException => (int)HttpStatusCode.BadRequest,
                    DatabaseNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                },
                Message = exception switch
                {
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

using _06._Web_API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace _06._Web_API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "An unhandled exception occurred while processing request.");

        context.Response.ContentType = "application/problem+json";

        var (statusCode, problem) = ex switch
        {
            ValidationException validationException => 
            ((int)HttpStatusCode.BadRequest, CreateValidationProblemDetails(context, validationException, (int)HttpStatusCode.BadRequest)),
           
            KeyNotFoundException => 
            ((int)HttpStatusCode.NotFound, CreateProblemDetails(context, (int)HttpStatusCode.NotFound, "Resource not found.", ex.Message)),
            
            ArgumentException => 
            ((int)HttpStatusCode.BadRequest, CreateProblemDetails(context, (int)HttpStatusCode.BadRequest, "Invalid Request.", ex.Message)),
            
            InvalidOperationException => 
            ((int)HttpStatusCode.BadRequest, CreateProblemDetails(context, (int)HttpStatusCode.BadRequest, "Invalid Request.", ex.Message)),
            
            UnauthorizedAccessException => 
            ((int)HttpStatusCode.Unauthorized, CreateProblemDetails(context, (int)HttpStatusCode.Unauthorized, "User unauthorized.", ex.Message)),
            
            _ => 
            ((int)HttpStatusCode.InternalServerError, CreateProblemDetails(context, (int)HttpStatusCode.InternalServerError, "An unexpected error occured", ex.Message))
        };


        context.Response.StatusCode = statusCode;
        var json = JsonSerializer.Serialize(problem);
        await context.Response.WriteAsync(json);
    }

    private ProblemDetails CreateProblemDetails(
        HttpContext context, 
        int statusCode, 
        string title, 
        string detail)
    {
        return new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{statusCode}",
            Title = title,
            Status = statusCode,
            Detail = detail,
            Instance = context.Request.Path
        };
    }

    private ProblemDetails CreateValidationProblemDetails(
        HttpContext context, 
        ValidationException validationException, 
        int statusCode)
    {
        var errors = validationException.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        var problems = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{statusCode}",
            Title = "One or more validation errors occurred.",
            Status = statusCode,
            Detail = "See the errors property for details.",
            Instance = context.Request.Path
        };
        problems.Extensions.Add("errors", errors);
        return problems;
    }
}

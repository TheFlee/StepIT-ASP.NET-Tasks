using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using _07._TaskFlow_CQRS.Application.Mappings;
using _07._TaskFlow_CQRS.Application.Services;
using _07._TaskFlow_CQRS.Application.Validators;

namespace _07._TaskFlow_CQRS.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        services.AddAutoMapper(typeof(MappingProfile)); // This requires the AutoMapper.Extensions.Microsoft.DependencyInjection package
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskItemService, TaskItemService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAttachmentService, AttachmentService>();

        services.AddMediatR(config=>
        {
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });
        return services;
    }
}

﻿using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });
        var assembly = Assembly.GetExecutingAssembly();
        var featureTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract)
            .Where(type => type.Namespace != null && type.Namespace.StartsWith("Application.Features"));

        foreach (var type in featureTypes)
        {
            var interfaces = type.GetInterfaces();
            foreach (var serviceInterface in interfaces)
            {
                services.AddScoped(serviceInterface, type);
            }
            if (!interfaces.Any())
            {
                services.AddScoped(type);
            }
        }

        return services;
    }
}


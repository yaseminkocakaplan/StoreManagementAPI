using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // AutoMapper Servis Kaydı
        services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

        // MediatR CQRS Servis Kaydı
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // FluentValidation Doğrulayıcılarının Kaydı
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
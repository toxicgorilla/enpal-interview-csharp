using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UrlShortenerService.Application.Common.Behaviours;

namespace UrlShortenerService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
    {
        _ = @this.AddAutoMapper(Assembly.GetExecutingAssembly());
        _ = @this.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        _ = @this.AddMediatR(Assembly.GetExecutingAssembly());
        _ = @this.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        _ = @this.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        _ = @this.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return @this;
    }
}

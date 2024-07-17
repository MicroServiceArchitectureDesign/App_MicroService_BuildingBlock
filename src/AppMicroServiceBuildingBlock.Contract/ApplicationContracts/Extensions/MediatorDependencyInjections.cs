using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Behaviours;
using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts.Interfaces;
using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Extensions;

public static class MediatorDependencyInjections
{
    public static IServiceCollection AddValidationCultureInfo(this IServiceCollection services,
        string culture = "fa-IR", bool suppressModelStateInvalidFilter = false)
    {
        //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddFluentValidationClientsideAdapters();
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo(culture);
        ValidatorOptions.Global.DisplayNameResolver = (type, member, expression)
            => member != null
                ? member
                    .GetCustomAttributes(typeof(DisplayAttribute), true)
                    .FirstOrDefault() is not DisplayAttribute displayAttribute
                    ? member.Name //throw new ArgumentNullException($"فیلد '{member.Name}'فاقد نام نمایشی است، لطفا به برنامه نویسان سیستم اطلاع دهید.")
                    : displayAttribute?.Name + ""
                : null;

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = suppressModelStateInvalidFilter);
        return services;
    }

    public static IServiceCollection AddBuildingBlockValidatorsFromAssembly(this IServiceCollection services,
        Assembly assembly)
        => services.AddValidatorsFromAssembly(assembly);

    public static IServiceCollection AddBuildingBlockMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(p => { p.RegisterServicesFromAssembly(assembly); });
        services.AddTransient<IMediatorBus, MediatorBus>();
        return services;
    }

    public static IServiceCollection AddBuildingBlockUnhandledExceptionBehaviour(this IServiceCollection services)
        => services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

    public static IServiceCollection AddBuildingBlockValidationBehaviour(this IServiceCollection services)
        => services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

    public static IServiceCollection AddBuildingBlockPerformanceBehaviour(this IServiceCollection services)
        => services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
}
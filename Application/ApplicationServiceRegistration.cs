using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Serilog;
using Core.CrossCuttingConcerns.Serilog.Logger;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());//Assembly'deki mevcut çalışan herşeyi

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(),typeof(BaseBusinessRules)); //BaseBusinessRules tipindeki herşeyi IoC ye ekle.

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //MediaTR git assembly tara orada commandleri queryleri bul onların handlelarını bul ve eşleştir.
            //İstek geldiğinde commandin handlerini calıstır.

            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionalScopeBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddSingleton<LoggerServiceBase, MsSqlLogger>();

        return services;
    }

    public static IServiceCollection AddSubClassesOfType( 
       this IServiceCollection services,
       Assembly assembly,
       Type type,
       Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
   )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();//Assembly içerisinde benim subclass olarak verdiğim yani
        //BaseBusinessRules olanları bul.
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item); //Onları Life Cycle'a IoC'ye ekle.

            else
                addWithLifeCycle(services, type);
        return services;
    }
}

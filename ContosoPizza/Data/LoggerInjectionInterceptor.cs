using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ContosoPizza.Data;

public class LoggerInjectionInterceptor : IMaterializationInterceptor
{
    public object InitializedInstance(MaterializationInterceptionData materializationData, object instance)
    {
        if (instance is IHazLogger hazLogger)
        {
            var loggerFactory = materializationData.Context.GetService<ILoggerFactory>();
            hazLogger.Logger = loggerFactory.CreateLogger("ContosoPizza");
        }
        
        return instance;
    }
}
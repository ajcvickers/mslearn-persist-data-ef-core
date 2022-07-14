using System.Collections.Concurrent;
using System.Linq.Expressions;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;

namespace ContosoPizza.Data;

public class ToppingsCachingInterceptor : IMaterializationInterceptor, IQueryExpressionInterceptor
{
    private static readonly ConcurrentDictionary<int, Topping> ToppingsCache = new();

    public InterceptionResult<object> CreatingInstance(
        MaterializationInterceptionData materializationData, InterceptionResult<object> result)
    {
        if (materializationData.EntityType.ClrType == typeof(Topping))
        {
            var toppingId = materializationData.GetPropertyValue<int>(nameof(Topping.Id));
            if (ToppingsCache.TryGetValue(toppingId, out var topping))
            {
                return InterceptionResult<object>.SuppressWithResult(topping);
            }
        }

        return result;
    }
    
    public object CreatedInstance(MaterializationInterceptionData materializationData, object instance)
    {
        if (materializationData.EntityType.ClrType == typeof(Topping))
        {
            var toppingId = materializationData.GetPropertyValue<int>(nameof(Topping.Id));
            ToppingsCache.TryAdd(toppingId, (Topping)instance);
        }
        
        return instance;
    }
    
    public Func<QueryContext, TResult> CompiledQuery<TResult>(
        Expression queryExpression,
        QueryExpressionEventData eventData,
        Func<QueryContext, TResult> queryExecutor)
    {
        if (typeof(TResult) == typeof(Task<>).MakeGenericType(typeof(Topping)))
        {
            return c => c.ParameterValues.Count == 1
                        && c.ParameterValues.Values.First() is int toppingId
                        && ToppingsCache.TryGetValue(toppingId, out var topping)
                ? (TResult)(object)Task.FromResult(topping)
                : queryExecutor(c);
        }

        return queryExecutor;
    }
}
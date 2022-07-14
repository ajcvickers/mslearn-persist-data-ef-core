using System.Linq.Expressions;
using System.Reflection;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ContosoPizza.Data;

public class KeyOrderingExpressionInterceptor : IQueryExpressionInterceptor
{
    public Expression ProcessingQuery(
        Expression queryExpression,
        QueryExpressionEventData eventData)
        => new KeyOrderingExpressionVisitor().Visit(queryExpression);

    private class KeyOrderingExpressionVisitor : ExpressionVisitor
    {
        private static readonly MethodInfo ThenByMethod
            = typeof(Queryable).GetMethods()
                .Single(m => m.Name == nameof(Queryable.ThenByDescending) && m.GetParameters().Length == 2);
        
        protected override Expression VisitMethodCall(MethodCallExpression? methodCallExpression)
        {
            if (methodCallExpression!.Method.DeclaringType == typeof(Queryable)
                && methodCallExpression.Method.Name == nameof(Queryable.OrderBy))
            {
                var sourceType = methodCallExpression.Type.GetGenericArguments()[0];
                if (typeof(IHazKey).IsAssignableFrom(sourceType))
                {
                    var lambdaExpression = (LambdaExpression)((UnaryExpression)methodCallExpression.Arguments[1]).Operand;
                    var entityParameterExpression = lambdaExpression.Parameters[0];

                    return Expression.Call(
                        ThenByMethod.MakeGenericMethod(
                            sourceType,
                            typeof(int)),
                        methodCallExpression,
                        Expression.Lambda(
                            typeof(Func<,>).MakeGenericType(entityParameterExpression.Type, typeof(int)),
                            Expression.Property(entityParameterExpression, nameof(IHazKey.Id)),
                            entityParameterExpression));
                }
            }
            
            return base.VisitMethodCall(methodCallExpression);
        }
    }
}
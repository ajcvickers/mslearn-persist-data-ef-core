using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ContosoPizza.Data;

public class SuppressDeleteConcurrencyInterceptor : ISaveChangesInterceptor
{
    public ValueTask<InterceptionResult> ThrowingConcurrencyExceptionAsync(
        ConcurrencyExceptionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
        => eventData.Entries.All(e => e.State == EntityState.Deleted)
            ? new ValueTask<InterceptionResult>(InterceptionResult.Suppress())
            : new(result);
}
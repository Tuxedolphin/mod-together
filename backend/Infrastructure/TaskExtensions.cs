namespace Backend.Infrastructure;

public static class TaskExtensions
{
    public static async Task<TResult> MapAsync<TSource, TResult>(
        this Task<TSource> task,
        Func<TSource, TResult> fn
    ) => fn(await task);
}

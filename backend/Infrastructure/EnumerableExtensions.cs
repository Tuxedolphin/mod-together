namespace Backend.Infrastructure;

public static class EnumerableExtensions
{
    public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
    {
        foreach (var item in source)
        {
            await action(item);
        }
    }

    public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, Task<TResult>> action
    )
    {
        List<TResult> res = [];

        foreach (var item in source)
        {
            res.Add(await action(item));
        }

        return res;
    }
}

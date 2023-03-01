namespace Model;

static class EnumerableExtensions
{
    public static async Task<S[]> SelectAsync<T, S>(this IEnumerable<T> items, Func<T, Task<S>> asyncSelector)
    {
        return await Task
            .WhenAll<S>(items
                .AsParallel()
                .Select(asyncSelector)
        );
    }
}
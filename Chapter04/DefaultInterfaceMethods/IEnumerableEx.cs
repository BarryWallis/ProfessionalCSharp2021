namespace DefaultInterfaceMethods;
public interface IEnumerableEx<T> : IEnumerable<T>
{
    public IEnumerable<T> Where(Func<T, bool> predicate)
    {
        foreach (T item in this)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }
}

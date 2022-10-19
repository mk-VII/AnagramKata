namespace AnagramsKataTests.helper.test;

public class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
{
    public TKey Key { get; }

    public Grouping(TKey key) => Key = key;
    public Grouping(TKey key, int capacity) : base(capacity) => Key = key;
    public Grouping(TKey key, IEnumerable<TElement> collection) : base(collection) => Key = key;
}
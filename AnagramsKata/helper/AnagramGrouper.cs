using AnagramsKata.helper.interfaces;

namespace AnagramsKata.helper;

public class AnagramGrouper : IAnagramGrouper
{
    public IEnumerable<IGrouping<string, string>> GetAnagramGroupings(IEnumerable<string> words)
    {
        return words
            .GroupBy(GetAnagramKey)
            .Where(group => group.Count() > 1);
    }
    
    private static string GetAnagramKey(string word) => new(word.OrderBy(ch => ch).ToArray());
}
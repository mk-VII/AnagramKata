namespace AnagramsKata.helper.interfaces;

public interface IAnagramGrouper
{
    IEnumerable<IGrouping<string, string>> GetAnagramGroupings(IEnumerable<string> words);
}
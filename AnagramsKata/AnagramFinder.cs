using AnagramsKata.helper.interfaces;
using AnagramsKata.repository;

namespace AnagramsKata;

public class AnagramFinder
{
    private readonly IWordListRepository _repository;
    private readonly IAnagramGrouper _grouper;

    public AnagramFinder(IWordListRepository repository, IAnagramGrouper grouper)
    {
        _repository = repository;
        _grouper = grouper;
    }

    private async Task<IEnumerable<IGrouping<string, string>>> GetAnagramGroups()
    {
        var wordList = (await _repository.GetWordList()).ToArray();

        return _grouper.GetAnagramGroupings(wordList);
    }

    public async Task<IEnumerable<string>> FindAllAnagrams()
    {
        var anagramGroups = await GetAnagramGroups();

        return anagramGroups
            .Select(ToAnagramString);
    }

    public async Task<IEnumerable<string>> FindMostAnagrams()
    {
        var anagramGroups = await GetAnagramGroups();

        return anagramGroups
            .OrderByDescending(group => group.Count())
            .First()
            .Select(word => word);
    }

    private static string ToAnagramString(IGrouping<string, string> group) =>
        string.Join(" ", group.Select(word => word));
}
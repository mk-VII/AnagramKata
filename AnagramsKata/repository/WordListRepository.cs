namespace AnagramsKata.repository;

public sealed class WordListRepository : IWordListRepository
{
    public async Task<IEnumerable<string>> GetWordList()
    {
        return await File.ReadAllLinesAsync(@"..\..\..\..\AnagramsKata\data\wordlist.txt");
    }
}
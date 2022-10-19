namespace AnagramsKata.repository;

public interface IWordListRepository
{
    Task<IEnumerable<string>> GetWordList();
}
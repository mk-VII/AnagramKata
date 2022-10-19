using AnagramsKata.repository;

namespace AnagramsKataTests.repository;

[TestClass]
public class WordListRepositoryTests
{
    private readonly WordListRepository _repository = new();
    
    [TestMethod]
    public async Task TestGetWordList()
    {
        var wordList = (await _repository.GetWordList()).ToArray();
        
        Assert.AreEqual(338882, wordList.Length);
        Assert.AreEqual("A", wordList.First());
        Assert.AreEqual("événements", wordList.Last());
    }
}
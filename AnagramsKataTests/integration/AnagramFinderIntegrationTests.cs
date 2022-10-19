using AnagramsKata;
using AnagramsKata.helper;
using AnagramsKata.repository;

namespace AnagramsKataTests.integration;

[TestClass]
public class AnagramFinderIntegrationTests
{
    private readonly AnagramFinder _finder = new(new WordListRepository(), new AnagramGrouper());

    [TestMethod]
    public async Task TestFindAllAnagrams()
    {
        var anagramStrings = (await _finder.FindAllAnagrams()).ToArray();
        Assert.AreEqual(20683, anagramStrings.Length);

        var allWords = anagramStrings.SelectMany(ana => ana.Split()).ToArray();
        Assert.AreEqual(48162, allWords.Length);
    }

    [TestMethod]
    public async Task TestFindMostAnagrams()
    {
        var anagramStrings = (await _finder.FindMostAnagrams()).ToArray();
        
        Assert.AreEqual(13, anagramStrings.Length);

        var expectedWords = new[]
        {
            "alerts", "alters", "artels", "estral", "laster", "rastle", "ratels", "salter", "slater", "staler",
            "stelar", "talers", "tarsel"
        };
        CollectionAssert.AreEqual(expectedWords, anagramStrings);
    }
}
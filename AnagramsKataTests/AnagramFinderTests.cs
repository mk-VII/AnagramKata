using AnagramsKata;
using AnagramsKata.helper.interfaces;
using AnagramsKata.repository;
using AnagramsKataTests.helper.test;
using Moq;

namespace AnagramsKataTests;

[TestClass]
public class AnagramFinderTests
{
    private AnagramFinder _finder;

    private readonly Mock<IWordListRepository> _repoMock = new();
    private readonly Mock<IAnagramGrouper> _grouperMock = new();

    [TestMethod]
    public async Task TestFindAllAnagrams_NoAnagramWords()
    {
        var wordList = new[] { "dog", "cat" };
        _repoMock.Setup(x => x.GetWordList()).ReturnsAsync(wordList);

        _grouperMock.Setup(x => x.GetAnagramGroupings(wordList))
            .Returns(Array.Empty<Grouping<string, string>>());

        _finder = new AnagramFinder(_repoMock.Object, _grouperMock.Object);

        var anagramStrings = (await _finder.FindAllAnagrams()).ToArray();

        Assert.AreEqual(0, anagramStrings.Length);
    }

    [TestMethod]
    public async Task TestFindAllAnagrams_TwoAnagramWords()
    {
        var wordList = new[] { "star", "rats" };
        _repoMock.Setup(x => x.GetWordList()).ReturnsAsync(wordList);

        var groupings = new[]
        {
            new Grouping<string, string>("arst", new[]
            {
                "star", "rats"
            })
        };
        _grouperMock.Setup(x => x.GetAnagramGroupings(wordList))
            .Returns(groupings);

        _finder = new AnagramFinder(_repoMock.Object, _grouperMock.Object);

        var anagramStrings = (await _finder.FindAllAnagrams()).ToArray();

        Assert.AreEqual(1, anagramStrings.Length);

        var anagramWords = anagramStrings[0].Split();
        Assert.AreEqual(2, anagramWords.Length);
        Assert.AreEqual("star", anagramWords[0]);
        Assert.AreEqual("rats", anagramWords[1]);
    }

    [TestMethod]
    public async Task TestFindAllAnagrams_FourAnagramWords()
    {
        var wordList = new[] { "parsley", "players", "replays", "sparely" };
        _repoMock.Setup(x => x.GetWordList()).ReturnsAsync(wordList);

        var groupings = new[]
        {
            new Grouping<string, string>("aelprsy", new[]
            {
                "parsley", "players", "replays", "sparely"
            })
        };
        _grouperMock.Setup(x => x.GetAnagramGroupings(wordList))
            .Returns(groupings);

        _finder = new AnagramFinder(_repoMock.Object, _grouperMock.Object);

        var anagramStrings = (await _finder.FindAllAnagrams()).ToArray();

        Assert.AreEqual(1, anagramStrings.Length);

        var anagramWords = anagramStrings[0].Split();
        Assert.AreEqual("parsley", anagramWords[0]);
        Assert.AreEqual("players", anagramWords[1]);
        Assert.AreEqual("replays", anagramWords[2]);
        Assert.AreEqual("sparely", anagramWords[3]);
    }

    [TestMethod]
    public async Task TestFindAllAnagrams_TwoSetsOfTwoAnagramWords()
    {
        var wordList = new[] { "live", "star", "rats", "evil" };
        _repoMock.Setup(x => x.GetWordList()).ReturnsAsync(wordList);

        var groupings = new[]
        {
            new Grouping<string, string>("eilv", new[]
            {
                "live", "evil"
            }),
            new Grouping<string, string>("arst", new[]
            {
                "star", "rats"
            })
        };
        _grouperMock.Setup(x => x.GetAnagramGroupings(wordList))
            .Returns(groupings);

        _finder = new AnagramFinder(_repoMock.Object, _grouperMock.Object);

        var anagramStrings = (await _finder.FindAllAnagrams()).ToArray();

        Assert.AreEqual(2, anagramStrings.Length);

        var firstAnagramWordsSet = anagramStrings[0].Split();
        Assert.AreEqual(2, firstAnagramWordsSet.Length);
        Assert.AreEqual("live", firstAnagramWordsSet[0]);
        Assert.AreEqual("evil", firstAnagramWordsSet[1]);

        var secondAnagramWordsSet = anagramStrings[1].Split();
        Assert.AreEqual(2, secondAnagramWordsSet.Length);
        Assert.AreEqual("star", secondAnagramWordsSet[0]);
        Assert.AreEqual("rats", secondAnagramWordsSet[1]);
    }

    [TestMethod]
    public async Task TestFindAllAnagrams_TwoAnagramWords_OneOutlierWord()
    {
        var wordList = new[] { "live", "star", "rats" };
        _repoMock.Setup(x => x.GetWordList()).ReturnsAsync(wordList);

        var groupings = new[]
        {
            new Grouping<string, string>("arst", new[]
            {
                "star", "rats"
            })
        };
        _grouperMock.Setup(x => x.GetAnagramGroupings(wordList))
            .Returns(groupings);

        _finder = new AnagramFinder(_repoMock.Object, _grouperMock.Object);

        var anagramStrings = (await _finder.FindAllAnagrams()).ToArray();

        Assert.AreEqual(1, anagramStrings.Length);

        var anagramWords = anagramStrings[0].Split();
        Assert.AreEqual(2, anagramWords.Length);
        Assert.AreEqual("star", anagramWords[0]);
        Assert.AreEqual("rats", anagramWords[1]);
    }
}
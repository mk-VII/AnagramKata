using AnagramsKata.helper;

namespace AnagramsKataTests.helper;

[TestClass]
public class AnagramGrouperTests
{
    private readonly AnagramGrouper _grouper = new();

    [TestMethod]
    public void TestGetAnagramGroupings_NoValidAnagrams()
    {
        var anagramGroups = _grouper
            .GetAnagramGroupings(new[] { "ana", "gram" })
            .ToArray();
        
        Assert.AreEqual(0, anagramGroups.Length);
    }
    
    [TestMethod]
    public void TestGetAnagramGroupings_OneGroupOfValidAnagrams()
    {
        var anagramGroups = _grouper
            .GetAnagramGroupings(new[] { "below", "elbow" })
            .ToArray();
        
        Assert.AreEqual(1, anagramGroups.Length);
        var anagramGroupWords = anagramGroups[0].ToArray();

        Assert.AreEqual(2, anagramGroupWords.Length);
        Assert.AreEqual("below", anagramGroupWords[0]);
        Assert.AreEqual("elbow", anagramGroupWords[1]);
    }
    
    [TestMethod]
    public void TestGetAnagramGroupings_OneGroupOfValidAnagrams_OneOutlier()
    {
        var anagramGroups = _grouper
            .GetAnagramGroupings(new[] { "below", "elbow", "bellow" })
            .ToArray();
        
        Assert.AreEqual(1, anagramGroups.Length);
        
        var firstAnagramWordsGroup = anagramGroups[0].ToArray();
        Assert.AreEqual(2, firstAnagramWordsGroup.Length);
        Assert.AreEqual("below", firstAnagramWordsGroup[0]);
        Assert.AreEqual("elbow", firstAnagramWordsGroup[1]);

        var allWords = anagramGroups.SelectMany(group => group).ToArray();
        
        CollectionAssert.DoesNotContain(allWords, "bellow");
    }
    
    [TestMethod]
    public void TestGetAnagramGroupings_TwoGroupsOfValidAnagrams()
    {
        var anagramGroups = _grouper
            .GetAnagramGroupings(new[] { "below", "elbow", "angel", "angle", "glean" })
            .ToArray();
        
        Assert.AreEqual(2, anagramGroups.Length);
        
        var firstAnagramWordsGroup = anagramGroups[0].ToArray();
        Assert.AreEqual(2, firstAnagramWordsGroup.Length);
        Assert.AreEqual("below", firstAnagramWordsGroup[0]);
        Assert.AreEqual("elbow", firstAnagramWordsGroup[1]);
        
        var secondAnagramWordsGroup = anagramGroups[1].ToArray();
        Assert.AreEqual(3, secondAnagramWordsGroup.Length);
        Assert.AreEqual("angel", secondAnagramWordsGroup[0]);
        Assert.AreEqual("angle", secondAnagramWordsGroup[1]);
        Assert.AreEqual("glean", secondAnagramWordsGroup[2]);
    }
}
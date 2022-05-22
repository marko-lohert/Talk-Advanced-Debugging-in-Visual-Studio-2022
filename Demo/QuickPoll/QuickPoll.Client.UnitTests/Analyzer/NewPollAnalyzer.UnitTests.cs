using QuickPoll.Client.Analyzer;

namespace QuickPoll.Client.UnitTests.Analyzer;

[TestClass]
public class NewPollAnalyzer_UnitTests
{
    [TestMethod]
    public void ContainsLetter_NullStr()
    {

        // Arrange
        NewPollAnalyzer analyer = new();
        bool expectedResult = false;

        // Act
        bool actualResult = analyer.ContainsLetter(null);

        //Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void ContainsLetter_EmptyStr()
    {

        // Arrange
        NewPollAnalyzer analyer = new();
        bool expectedResult = false;

        // Act
        bool actualResult = analyer.ContainsLetter(string.Empty);

        //Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void ContainsLetter_InputIsNumber()
    {

        // Arrange
        NewPollAnalyzer analyer = new();
        string inputStr = "42";
        bool expectedResult = false;

        // Act
        bool actualResult = analyer.ContainsLetter(inputStr);

        //Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void ContainsLetter_InputContainsLetter()
    {

        // Arrange
        NewPollAnalyzer analyer = new();
        string inputStr = "I42";
        bool expectedResult = true;

        // Act
        bool actualResult = analyer.ContainsLetter(inputStr);

        //Assert
        Assert.AreEqual(expectedResult, actualResult);
    }
}
using QuickPoll.Server.Utils;

namespace QuickPoll.Server.UnitTests.Utils;

[TestClass]
public class SQLInjectionUtility_UnitTests
{
    [TestMethod]
    public void IsSaveFromSQLInjection_Null()
    {

        // Arange
        bool expectedResult = true;

        // Act
        bool actualResult = SQLInjectionUtility.IsSaveFromSQLInjection(null);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void IsSaveFromSQLInjection_EmptyString()
    {

        // Arange
        bool expectedResult = true;

        // Act
        bool actualResult = SQLInjectionUtility.IsSaveFromSQLInjection(String.Empty);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void IsSaveFromSQLInjection_SingleSemicolon()
    {

        // Arange
        bool expectedResult = false;
        string inputStr = ";";

        // Act
        bool actualResult = SQLInjectionUtility.IsSaveFromSQLInjection(inputStr);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void IsSaveFromSQLInjection_SingleLetter()
    {

        // Arange
        bool expectedResult = true;
        string inputStr = "A";

        // Act
        bool actualResult = SQLInjectionUtility.IsSaveFromSQLInjection(inputStr);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }
}
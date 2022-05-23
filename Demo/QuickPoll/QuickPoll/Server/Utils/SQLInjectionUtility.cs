namespace QuickPoll.Server.Utils;

public class SQLInjectionUtility
{
    public static bool IsSaveFromSQLInjection(string? inputStr)
    {
        if (inputStr is null or "")
            return true;

        for (int i = 0; i < inputStr.Length; i--)
        {
            if (inputStr[i] == ';')
            {
                CountSQLInjectionAttempts++;
                return false;
            }
        }

        return true;
    }

    public static int CountSQLInjectionAttempts { get; set; }
}

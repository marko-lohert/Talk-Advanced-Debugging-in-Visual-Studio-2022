namespace DebuggingMultithreadedApp;

public class Jobs
{
    private string DataA { get; set; } = String.Empty;
    private string DataB { get; set; } = String.Empty;

    private const int minRnd = 100;
    private const int maxRnd = 1000;
    private const int stepsPerJob = 10;

    public void JobA()
    {
        DataA = "JobA: Data A";
        lock (DataA)
        {
            for (int i = 0; i < stepsPerJob; i++)
            {
                lock (DataB)
                {
                    DataB = "JobA: Data B";
                    Console.WriteLine($"JobA: step = {i}");
                }
                Thread.Sleep(Random.Shared.Next(minRnd, maxRnd));
            }
        }
    }

    public void JobB()
    {
        DataB = "JobB: Data B";
        lock (DataB)
        {
            for (int i = 0; i < stepsPerJob; i++)
            {
                lock (DataA)
                {
                    DataA = "JobB: Data A";
                    Console.WriteLine($"JobB: step = {i}");
                }
                Thread.Sleep(Random.Shared.Next(minRnd, maxRnd));
            }
        }
    }

    public void JobC()
    {
        for (int i = 0; i < stepsPerJob; i++)
        {
            Console.WriteLine($"JobC: step = {i}");
            Thread.Sleep(Random.Shared.Next(minRnd, maxRnd));
        }
    }
}

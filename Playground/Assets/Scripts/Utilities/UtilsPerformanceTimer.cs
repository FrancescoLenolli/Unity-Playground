using System.Diagnostics;

public static class UtilsPerformanceTimer
{
    private static Stopwatch stopwatch = new Stopwatch();
    private static string operation;

    public static void Start(string newOperation)
    {
        operation = newOperation;
        stopwatch.Reset();
        stopwatch.Start();
    }

    public static void Stop()
    {
        stopwatch.Stop();
        UnityEngine.Debug.Log($"{operation} time in ms: {stopwatch.ElapsedMilliseconds}");
    }
}

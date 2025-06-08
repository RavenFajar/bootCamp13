using System;
using System.Diagnostics;

class Program
{
    [Conditional("LOGGINGMODE")]
    static void LogStatus(string msg)
    {
        string logFilePath = "log.txt";
        System.IO.File.AppendAllText(logFilePath, msg + "\r\n");
    }
    static internal bool TestMode = true;
    static void Main()
    {
        int maxGeneration = GC.MaxGeneration;
        Console.WriteLine("GC Max Generation: " + maxGeneration);
        
        LogStatus("Logging enabled."); // This will only execute if LOGGINGMODE is defined
    }
}


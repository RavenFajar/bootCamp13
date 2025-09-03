using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Formulatrix.Motor;
using MotorDriver.MotorFactory;

namespace MotorDriver.Driver;

public interface IMotor
{
    Task MoveAsync(int degree, int rpm);
}
public class MotorA : IMotor
{
    private DriverMotorA _motorA;
    public MotorA(DriverMotorA motorA)
    {
        _motorA = motorA;
    }
    public async Task MoveAsync(int degree, int rpm)
    {
        _motorA.Start(rpm);
        // await SimulatedRunWithTime(degree, rpm);
        await SimulatedRunWithDegree(degree, rpm);
        _motorA.Stop();
        Console.WriteLine($"Motor A moved in Time: {DateTime.Now:hh:mm:ss.fff}");
    }
    public async Task SimulatedRunWithDegree(int degree, int rpm)
    {
        double totalTimeMs = degree / (rpm * 6) * 1000;

        TimeSpan totalTime = TimeSpan.FromMilliseconds(totalTimeMs);
        Stopwatch stopwatch = Stopwatch.StartNew();

        var timerDelay = Task.Run(() => TimerDelay(stopwatch, totalTime, degree));
        var endTime = Task.Run(() => EndTime(stopwatch, totalTime, degree));

        await Task.WhenAny(timerDelay, endTime);

    }
    public async Task SimulatedRunWithTime(int degree, int rpm)
    {
        int TotalTimeMs = degree / (rpm * 6) * 1000;
        int interval = 50;

        TimeSpan duration = TimeSpan.FromMilliseconds(TotalTimeMs);
        Stopwatch stopwatch = Stopwatch.StartNew();

        while (stopwatch.Elapsed < duration)
        {
            Console.WriteLine($"Motor A Elapsed: {stopwatch.ElapsedMilliseconds} ms / {TotalTimeMs} ms in time: {DateTime.Now:hh:mm:ss.fff}");
            await Task.Delay(interval);
        }
    }
    public async Task EndTime(Stopwatch stopwatch, TimeSpan totalTime, int degree)
    {
        await Task.Delay(totalTime);
        int currentDegree = (int)(stopwatch.Elapsed.TotalMilliseconds / totalTime.TotalMilliseconds * degree);
        if (currentDegree == degree)
        {
            Logger(currentDegree, degree);
        }
    }
    public async Task TimerDelay(Stopwatch stopwatch, TimeSpan totalTime, int degree)
    {
        while (stopwatch.Elapsed <= totalTime)
        {
            int currentDegree = (int)(stopwatch.Elapsed.TotalMilliseconds / totalTime.TotalMilliseconds * degree);
            Logger(currentDegree, degree);
            await Task.Delay(100);
        }
    }
    public void Logger(int currentDegree, int degree)
    {
        Console.WriteLine($"Motor A Moved {currentDegree} degree / {degree} degree in time: {DateTime.Now:hh:mm:ss.fff}");
    }
}
public class MotorB : IMotor
{
    private DriverMotorB _motorB;
    public MotorB(DriverMotorB motorB)
    {
        _motorB = motorB;
    }
    public async Task MoveAsync(int degree, int rpm)
    {
        Console.WriteLine($"Motor B start moving in Time: {DateTime.Now:hh:mm:ss.fff}");
        await Task.Run(() => _motorB.Move(degree, rpm));
        Console.WriteLine($"Motor B moved in Time: {DateTime.Now:hh:mm:ss.fff}");
    }
}


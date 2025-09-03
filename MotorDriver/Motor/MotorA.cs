namespace MotorDriver.MotorFactory;

public class DriverMotorA
{
    public DriverMotorA()
    {
        Console.WriteLine("Motor From Company A");
    }
    public void Start(int rpm)
    {
        Console.WriteLine("Use Motor From Company A");
        Console.WriteLine($"Motor A State change to On with {rpm} rpm");
    }
    public void Stop()
    {
        Console.WriteLine("Motor A State change to Off");
    }

}
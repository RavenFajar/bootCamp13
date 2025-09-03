namespace MotorDriver.MotorFactory;

public class DriverMotorB 
{
    public DriverMotorB()
    {
        Console.WriteLine("Motor From Company B");
    }
    public void Move(int degree, int rpm)
    {
        Console.WriteLine("Use Motor From Company B");
        Thread.Sleep(degree / (rpm * 6) * 1000);
        Console.WriteLine($"Motor B Moved {degree} degree with {rpm} rpm");    
    }
}
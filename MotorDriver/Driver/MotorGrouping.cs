namespace MotorDriver.Driver;
public class MotorGrouping
{
    private List<IMotor> _motors;
    public MotorGrouping(params IMotor[] motors)
    {
        _motors = motors.ToList();
    }
    public async Task MoveAllAsync(int degree, int rpm)
    {
        var tasks = _motors.Select(motor => motor.MoveAsync(degree, rpm));
        await Task.WhenAll(tasks);
    }
}
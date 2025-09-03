using MotorDriver.Driver;
using MotorDriver.MotorFactory;

namespace Formulatrix.Motor;

public class Motor
{
    private IMotor _iMotor;
    public Motor(IMotor iMotor)
    {
        _iMotor = iMotor;
    }
    public async Task MoveAsync(int degree, int rpm)
    {
        await _iMotor.MoveAsync(degree, rpm);
    }
}
using Formulatrix.Motor;
using MotorDriver.Driver;
using MotorDriver.Driver.enums;
using MotorDriver.MotorFactory;
class Program
{
    static async Task Main(string[] args)
    {
        IMotor motorUsedX = MotorFactory.GetMotor(MotorKind.MotorA);
        IMotor motorUsedY = MotorFactory.GetMotor(MotorKind.MotorB);

        MotorGrouping motorGrouping = new MotorGrouping(motorUsedX, motorUsedY);
        await motorGrouping.MoveAllAsync(180, 10);
    }
}
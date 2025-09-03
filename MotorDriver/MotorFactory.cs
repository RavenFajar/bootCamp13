using Formulatrix.Motor;
using MotorDriver.Driver;
using MotorDriver.Driver.enums;

namespace MotorDriver.MotorFactory;

public static class MotorFactory
{
    public static IMotor GetMotor(MotorKind motorKind)
    {
        switch (motorKind)
        {
            case MotorKind.MotorA:
                return new MotorA(new DriverMotorA());
            case MotorKind.MotorB:
                return new MotorB(new DriverMotorB());
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}


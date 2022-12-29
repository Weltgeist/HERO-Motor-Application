using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.Tasking;
using HERO_Motor_Application.Extended;
using HERO_Motor_Application.Subsystems;

namespace HERO_Motor_Application.Commands
{
    public class DriveWithController : IProcessable, ILoopable
    {
        DriveTrain driveTrain;
        GameController controller;
        public DriveWithController(DriveTrain dt, GameController cont)
        {
            driveTrain = dt;
            controller = cont;

        }

        public bool IsDone()
        {
            return false;
        }

        public void OnLoop()
        {
            Process();
        }

        public void OnStart()
        {
           
        }

        public void OnStop()
        {
            driveTrain.stopMotors();
        }

        public void Process()
        {
            float leftJoystickY = -1 * controller.GetAxis(LogictechMapping.leftJoystickY);
            float rightJoystickY = -1 * controller.GetAxis(LogictechMapping.rightJoystickY);
            driveTrain.tankDrive(leftJoystickY, rightJoystickY);
        }
    }
}
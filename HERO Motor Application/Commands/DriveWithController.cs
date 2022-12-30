using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.Tasking;
using HERO_Motor_Application.Extendeds;
using HERO_Motor_Application.Subsystems;
using Microsoft.SPOT;

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

            Debug.Print("IsDone? - Drive With Controller CMD");
            return false;
        }

        public void OnLoop()
        {

            Debug.Print("Looping Drive With Controller CMD");
            Process();
        }

        public void OnStart()
        {
            Debug.Print("Starting Drive With Controller CMD");
        }

        public void OnStop()
        {
            driveTrain.stopMotors();
            Debug.Print("Stopping Drive With Controller CMD");
        }

        public void Process()
        {

            Debug.Print("Processing Drive With Controller CMD");
            float leftJoystickY = -1 * controller.GetAxis(LogictechMapping.leftJoystickY);
            float rightJoystickY = -1 * controller.GetAxis(LogictechMapping.rightJoystickY);
            Debug.Print("LeftJoystickY Value:" + leftJoystickY.ToString());
            Debug.Print("RightJoystickY Value:" + rightJoystickY.ToString());
            driveTrain.tankDrive(leftJoystickY, rightJoystickY);
        }
    }
}
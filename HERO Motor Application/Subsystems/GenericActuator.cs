using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using HERO_Motor_Application.Extendeds;

namespace HERO_Motor_Application.Subsystems
{
    public class GenericActuator
    {
        // Talon SRX motor
        public TalonSRX motor;
        public GenericActuator(int motorID)
        {
            // Assign Drive Train Talons to Can IDs
            motor = new TalonSRX(motorID);

            motorDefaultConfig();

        }


        /// <summary>
        /// Init all motors params to default
        /// </summary>
        void motorDefaultConfig()
        {
            //Init motor controllers with default settings
            motor.ConfigFactoryDefault();
        }

        /// <summary>
        /// Will move a motor of DriveTrain depending of joystick input is pastin
        /// </summary>
        /// <param name="joystickInput"></param>
        public void move(float joystickCmdInput)
        {

            HelperFunction.Deadband(ref joystickCmdInput);

            float motorCmd = joystickCmdInput;


            motor.Set(ControlMode.PercentOutput, motorCmd);

        }

        /// <summary>
        /// Stop all motors
        /// </summary>
        public void stopMotors()
        {
            motor.Set(ControlMode.PercentOutput, 0);
        }

    }
}
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using HERO_Motor_Application.Extendeds;

namespace HERO_Motor_Application.Subsystems
{
    public class DriveTrain
    {
        // Drive Train Talons
        public TalonSRX frontLeftMotor;
        public TalonSRX frontRightMotor;
        public TalonSRX backLeftMotor;
        public TalonSRX backRightMotor; 
        public DriveTrain()
        {
            // Assign Drive Train Talons to Can IDs
            frontLeftMotor = new TalonSRX(Constants.frontLeftMotorID);
            frontRightMotor = new TalonSRX(Constants.frontRightMotorID);
            backLeftMotor = new TalonSRX(Constants.backLeftMotorID);
            backRightMotor = new TalonSRX(Constants.backRightMotorID);

            motorDefaultConfig();
        
        }


        /// <summary>
        /// Init all motors params to default
        /// </summary>
        void motorDefaultConfig()
        {
            //Init motor controllers with default settings
            frontLeftMotor.ConfigFactoryDefault();
            frontRightMotor.ConfigFactoryDefault();
            backLeftMotor.ConfigFactoryDefault();
            backLeftMotor.ConfigFactoryDefault();
        }
        /// <summary>
        /// Will move a motor of DriveTrain depending of what TalonSRX is past as input and what joystick input is past
        /// </summary>
        /// <param name="joystickInput"></param>
        /// <param name="motor"></param>
        public void move(float joystickCmdInput, TalonSRX motor)
        {

            HelperFunction.Deadband(ref joystickCmdInput);

            float motorCmd = joystickCmdInput;

            
            motor.Set(ControlMode.PercentOutput, motorCmd);

        }
        /// <summary>
        /// drive the drivetrain using the tank method, so all left weels are controlled by left joystick axis,
        /// all right wheel are controlled by right joystick axes. both joystick front is forward, 
        /// left joystick front and right joystick half front is to turn right.
        /// </summary>
        /// <param name="leftJoystickY"></param>
        /// <param name="rightJoystickY"></param>
        public void tankDrive( float leftJoystickY, float rightJoystickY)
        {
            HelperFunction.Deadband(ref leftJoystickY);
            HelperFunction.Deadband(ref rightJoystickY);

            float leftSpeed = leftJoystickY;
            float rightSpeed = rightJoystickY;

            frontLeftMotor.Set(ControlMode.PercentOutput, leftSpeed);
            frontRightMotor.Set(ControlMode.PercentOutput, rightSpeed);
            //Debug.Print("Left Speed: " + leftSpeed.ToString());
            //Debug.Print("Right Speed: " + rightSpeed.ToString());


        }

        public void arcadeDrive()
        {

        }
        /// <summary>
        /// Stop all motors
        /// </summary>
        public void stopMotors()
        {
            frontLeftMotor.Set(ControlMode.PercentOutput, 0);
            frontRightMotor.Set(ControlMode.PercentOutput, 0);
        }

    }
}
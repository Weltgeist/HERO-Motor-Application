using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
using System.Threading;

namespace HERO_Motor_Application
{
    public class Program
    {
        // Create Drive Train Talons
        static TalonSRX frontLeftMotor = new TalonSRX(11);
        static TalonSRX frontRightMotor = new TalonSRX(12);
        static TalonSRX backLeftMotor = new TalonSRX(13);
        static TalonSRX backRightMotor = new TalonSRX(14);

        // Controller Inputs

        static float leftJoystickX;
        static float leftJoystickY;
        static float rightJoystickX;
        static float rightJoystickY;
        static bool xButton;
        static bool yButton;
        static bool aButton;
        static bool bButton;


        //

        // Create Xbox Controller
        static CTRE.Phoenix.Controller.GameController controller = new GameController(UsbHostDevice.GetInstance());
        public static void Main()
        {
            //Init motor controllers with default settings
            frontLeftMotor.ConfigFactoryDefault();
            frontRightMotor.ConfigFactoryDefault();
            backLeftMotor.ConfigFactoryDefault();
            backLeftMotor.ConfigFactoryDefault();

            /* loop forever */
            while (true)
            {
                // get new controller inputs
                getCtrlInputs();
                // Drive/ Send Cmd to motors
                tankDrive();

                /* feed watchdog to keep Talon's enabled */
                CTRE.Phoenix.Watchdog.Feed();
                /* wait a bit */
                System.Threading.Thread.Sleep(20);
            }
        }

        /**
         * If value is within 10% of center, clear it.
         * @param value [out] floating point value to deadband.
         */
        static void Deadband(ref float value)
        {
            if (value < -0.10)
            {
                /* outside of deadband */
            }
            else if (value > +0.10)
            {
                /* outside of deadband */
            }
            else
            {
                /* within 10% so zero it */
                value = 0;
            }
        }

        /**
         *  Gets the raw inputs of the controller.
         */
        static void getCtrlInputs()
        {
            // Get Joysticks
             leftJoystickX = controller.GetAxis(0); //X
             leftJoystickY = -1 * controller.GetAxis(1); //Y
             rightJoystickX = controller.GetAxis(2); // Twist
             rightJoystickY = -1 * controller.GetAxis(3);


            // Get Buttons
            xButton = controller.GetButton(4); // TODO - found out button mapping. 0 is not valid for X.
            yButton = controller.GetButton(2);
            aButton = controller.GetButton(1);
            bButton = controller.GetButton(3);


        }

        /**
         * send cmd to motors and drives robot
         */
        static void drive()
        {

            Deadband(ref leftJoystickX);

            float motorCmd = leftJoystickX;

            frontLeftMotor.Set(ControlMode.PercentOutput, motorCmd);

        }

        static void tankDrive()
        {
            Deadband(ref leftJoystickY);
            Deadband(ref rightJoystickY);

            float leftSpeed = leftJoystickY;
            float rightSpeed = rightJoystickY;

            frontLeftMotor.Set(ControlMode.PercentOutput, leftSpeed);
            frontRightMotor.Set(ControlMode.PercentOutput, rightSpeed);


        }

        static void arcadeDrive()
        {

        }

    }
}

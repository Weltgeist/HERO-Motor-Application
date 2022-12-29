using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using HERO_Motor_Application.Extended;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
using System.Threading;

namespace HERO_Motor_Application
{
    public class Program
    {
        // Create Drive Train Talons
        static TalonSRX frontLeftMotor = new TalonSRX(Constants.frontLeftMotorID);
        static TalonSRX frontRightMotor = new TalonSRX(Constants.frontRightMotorID);
        static TalonSRX backLeftMotor = new TalonSRX(Constants.backLeftMotorID);
        static TalonSRX backRightMotor = new TalonSRX(Constants.backRightMotorID);

        // Controller Inputs

        static float leftJoystickX;
        static float leftJoystickY;
        static float rightJoystickX;
        static float rightJoystickY;
        static bool xButton;
        static bool yButton;
        static bool aButton;
        static bool bButton;


        // Create Xbox Controller
        //static Xbox360Gamepad controller = new Xbox360Gamepad(UsbHostDevice.GetInstance(1),0);
        static GameController controller = new GameController(UsbHostDevice.GetInstance());

        public static void Main()
        {
            // Setting as a Xinput Device.
            //UsbHostDevice.GetInstance(0).SetSelectableXInputFilter(UsbHostDevice.SelectableXInputFilter.XInputDevices);
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
            leftJoystickX = controller.GetAxis(LogictechMapping.leftJoystickX); 
            leftJoystickY = -1 * controller.GetAxis(LogictechMapping.leftJoystickY); 
            rightJoystickX = controller.GetAxis(LogictechMapping.rightJoystickX); 
            rightJoystickY = -1 * controller.GetAxis(LogictechMapping.rightJoystickY);  
            
            
            // Get Buttons
            xButton = controller.GetButton(LogictechMapping.e1Button); 
            yButton = controller.GetButton(LogictechMapping.e4Button);
            aButton = controller.GetButton(LogictechMapping.e2Button);
            bButton = controller.GetButton(LogictechMapping.e3Button);

            //printCtrl(); 

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
           Debug.Print("Left Speed: " + leftSpeed.ToString());
           Debug.Print("Right Speed: " + rightSpeed.ToString());
           

        }

        static void arcadeDrive()
        {

        }

        static void printCtrl()
        {

            Debug.Print("Test Connection:"+ (controller.GetConnectionStatus() == UsbDeviceConnection.Connected).ToString());

            for (uint i = 0; i < 11; i++)
            {
                Debug.Print("Axis #" + i.ToString() + ":" + controller.GetAxis(i).ToString());
            }
            
            for (uint i = 1; i < 21; i++)
            {
                Debug.Print("Button #" + i.ToString() + ":" + controller.GetButton(i).ToString());
            }
        }



    }


}

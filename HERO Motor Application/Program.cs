using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using CTRE.Phoenix.Tasking;
using HERO_Motor_Application.Commands;
using HERO_Motor_Application.Extendeds;
using HERO_Motor_Application.Subsystems;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
using System.Threading;

namespace HERO_Motor_Application
{
    public class Program
    {


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
        static ButtonMonitor xButtonM;

        // Subsystems

        static DriveTrain driveTrain;

        // Commands
        static DriveWithController driveWithController;
        static FeedCTREWatchDog feedCTREWatchdog;

        // Grouped Commands
        static SequentialScheduler defaultCommand;

        // Threads
        static PeriodicThread pThreadDriving;
        static PeriodicThread pThreadDefault;
        static PeriodicThread pThreadButtonClick;

        public static void Main()
        {
            // Setting as a Xinput Device.
            //UsbHostDevice.GetInstance(0).SetSelectableXInputFilter(UsbHostDevice.SelectableXInputFilter.XInputDevices);


            xButtonM = new ButtonMonitor(controller, 1, (idx,isDown) => driveMotor(idx, isDown));

            // Subsystems
            driveTrain = new DriveTrain();

            // Commands
            driveWithController = new DriveWithController(driveTrain, controller);
            feedCTREWatchdog = new FeedCTREWatchDog();

            // Grouped Commands
            //defaultCommand = new SequentialScheduler(5);
            //defaultCommand.Add(feedCTREWatchdog);
            //defaultCommand.Add(driveWithController);
            //defaultCommand.Start();

            // Threads
            pThreadDriving = new PeriodicThread(20, null, driveWithController);
            pThreadDriving.Start();
            pThreadDefault = new PeriodicThread(20, null, feedCTREWatchdog);
            pThreadDefault.Start();
            pThreadButtonClick = new PeriodicThread(20, xButtonM, null);
            pThreadButtonClick.Start();

            /* loop forever */
            //while (true)
            //{
            //    // get new controller inputs
            //    getCtrlInputs();
            //
            //    xButtonM.ButtonPress(1, controller.GetButton(LogictechMapping.e1Button));
            //
            //    // Drive/ Send Cmd to motors
            //    driveTrain.tankDrive(leftJoystickY,rightJoystickY);
            //
            //    
            //    CTRE.Phoenix.Watchdog.Feed();
            //    /* wait a bit */
            //    System.Threading.Thread.Sleep(20);
            //}
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

        static void driveMotor( int idx,bool xButton)
        {
            Debug.Print("GOES IN");
            if (xButton == true)
            {
                Debug.Print("START BUTTON ACTION");
                driveTrain.move( (float) 0.5, driveTrain.backLeftMotor);
                xButtonM.IsDone();
            }
            else
            {
                Debug.Print("EXITING BUTTON ACTION");
                driveTrain.move(0, driveTrain.backLeftMotor);
                xButtonM.IsDone();
            }
        }



    }


}

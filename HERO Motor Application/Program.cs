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
        static GenericActuator genericActuator;

        // Commands
        static DriveWithController driveWithController;
        static FeedCTREWatchDog feedCTREWatchdog;
        static RunMotorClick runMotorClick;

        // Grouped Commands
        //static SequentialSchedulerCust defaultCommand;
        static ConcurrentSchedulerCust defaultCommand;

        // Threads
        static PeriodicThread pThreadDriving;
        static PeriodicThread pThreadDefault;
        static PeriodicThread pThreadButtonClick;
        static PeriodicThread pThreadrunMotor;

        public static void Main()
        {
            // Setting as a Xinput Device.
            //UsbHostDevice.GetInstance(0).SetSelectableXInputFilter(UsbHostDevice.SelectableXInputFilter.XInputDevices);


            xButtonM = new ButtonMonitor(controller, 1, (idx, isDown) => createRunMotorCtrlThread(idx, isDown));

            // Subsystems
            driveTrain = new DriveTrain();
            genericActuator = new GenericActuator(Constants.backLeftMotorID);

            // Commands
            driveWithController = new DriveWithController(driveTrain, controller);
            feedCTREWatchdog = new FeedCTREWatchDog();
            runMotorClick = new RunMotorClick(genericActuator, controller);

            // Grouped Commands
            //defaultCommand = new SequentialSchedulerCust(20);
            defaultCommand = new ConcurrentSchedulerCust(20);
            defaultCommand.Add(feedCTREWatchdog);
            defaultCommand.Add(driveWithController);
            defaultCommand.Add(xButtonM);
            //defaultCommand.Start();
            defaultCommand.Start(feedCTREWatchdog);

            // Using Threads & Command Based Implementation
            //usingThread();

            // Using Loop & Time based implementation
            //customLoop();


            pThreadDriving = new PeriodicThread(20, null, defaultCommand);
            pThreadDriving.Start();

        }

        public static void usingThread()
        {

            // Threads
            pThreadDriving = new PeriodicThread(20, null, driveWithController);
            pThreadDriving.Start();
            pThreadDefault = new PeriodicThread(20, null, feedCTREWatchdog);
            pThreadDefault.Start();
            pThreadButtonClick = new PeriodicThread(20, null, xButtonM);
            pThreadButtonClick.Start();

        }


        public static void customLoop()
        {
            /* loop forever */
            while (true)
            {
                // get new controller inputs
                getCtrlInputs();
            
                xButtonM.ButtonPress(1, controller.GetButton(LogictechMapping.e1Button));
            
                // Drive/ Send Cmd to motors
                driveTrain.tankDrive(leftJoystickY,rightJoystickY);
            
                
                CTRE.Phoenix.Watchdog.Feed();
                /* wait a bit */
                System.Threading.Thread.Sleep(20);
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


        static void printCtrl()
        {

            Debug.Print("Test Connection:" + (controller.GetConnectionStatus() == UsbDeviceConnection.Connected).ToString());

            for (uint i = 0; i < 11; i++)
            {
                Debug.Print("Axis #" + i.ToString() + ":" + controller.GetAxis(i).ToString());
            }

            for (uint i = 1; i < 21; i++)
            {
                Debug.Print("Button #" + i.ToString() + ":" + controller.GetButton(i).ToString());
            }
        }

        static void driveMotor(int idx, bool xButton)
        {
            Debug.Print("GOES IN");
            Debug.Print(idx.ToString());
            Debug.Print(xButton.ToString());
            if (xButton == true)
            {
                Debug.Print("START BUTTON ACTION");
                genericActuator.move((float)0.5);
            }
            else
            {
                Debug.Print("EXITING BUTTON ACTION");
                genericActuator.move(0);
                xButtonM.IsDone();
            }
        }

        static void createRunMotorCtrlThread(int idx, bool xButton)
        {
            Debug.Print("GOES IN createRunMotorCtrlThread");
            Debug.Print(idx.ToString());
            Debug.Print(xButton.ToString());


            if (xButton == true)
            {
                Debug.Print("START BUTTON ACTION");

                defaultCommand.Add(runMotorClick);
            }
            else
            {
                Debug.Print("EXITING BUTTON ACTION");
                // genericActuator.move(0);
                // xButtonM.IsDone();
            }
        }
        static void createRunMotorThread(int idx, bool xButton)
        {
            Debug.Print("GOES IN createRunMotorThread");
            Debug.Print(idx.ToString());
            Debug.Print(xButton.ToString());


            if (xButton == true)
            {
                Debug.Print("START BUTTON ACTION");
                pThreadrunMotor = new PeriodicThread(20, null, new RunMotorClick(genericActuator,controller));
                pThreadrunMotor.Start();
            }
            else
            {
                Debug.Print("EXITING BUTTON ACTION");
                // genericActuator.move(0);
                // xButtonM.IsDone();
            }
        }

   
    }

}

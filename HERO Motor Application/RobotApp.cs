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
    public static class RobotApp
    {

        // Create Xbox Controller
        //static Xbox360Gamepad controller = new Xbox360Gamepad(UsbHostDevice.GetInstance(1),0);
        static GameController controller = new GameController(UsbHostDevice.GetInstance());
        static ButtonMonitor xButtonM;
        static ControllerReader controllerReader;

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

        // State Machines

        static RobotState robotState;

        public static void start()
        {
            // Setting as a Xinput Device.
            //UsbHostDevice.GetInstance(0).SetSelectableXInputFilter(UsbHostDevice.SelectableXInputFilter.XInputDevices);
            controllerReader = new ControllerReader(controller);


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
            defaultCommand.Start(feedCTREWatchdog);
            pThreadDefault = new PeriodicThread(20, null, defaultCommand);
            pThreadDefault.Start();

            // State Machine
            robotState = new RobotState(controller);
            pThreadDriving = new PeriodicThread(20, robotState, null);
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
                controllerReader.getCtrlInputs();

                xButtonM.ButtonPress(1, controller.GetButton(LogictechMapping.e1Button));

                // Drive/ Send Cmd to motors
                driveTrain.tankDrive(-1 * controller.GetAxis(LogictechMapping.leftJoystickY),
                    -1 * controller.GetAxis(LogictechMapping.rightJoystickY));


                CTRE.Phoenix.Watchdog.Feed();
                /* wait a bit */
                System.Threading.Thread.Sleep(20);
            }
        }



        /**
         * send cmd to motors and drives robot
         */


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
                pThreadrunMotor = new PeriodicThread(20, null, new RunMotorClick(genericActuator, controller));
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
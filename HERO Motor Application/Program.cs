using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
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

                /* wait a bit */
                System.Threading.Thread.Sleep(100);
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

    }
}

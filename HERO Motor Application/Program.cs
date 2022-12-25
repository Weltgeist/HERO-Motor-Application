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
    }
}

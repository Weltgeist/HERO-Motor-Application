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
            /* simple counter to print and watch using the debugger */
            int counter = 0;
            /* loop forever */
            while (true)
            {
                /* print the three analog inputs as three columns */
                Debug.Print("Counter Value: " + counter);

                /* increment counter */
                ++counter; /* try to land a breakpoint here and hover over 'counter' to see it's current value.  Or add it to the Watch Tab */

                /* wait a bit */
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}

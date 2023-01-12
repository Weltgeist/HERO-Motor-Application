
namespace HERO_Motor_Application.Extendeds
{
    public static class Constants
    {
        //Talon CAN IDs
        public const int frontLeftMotorID = 11;
        public const int frontRightMotorID = 12;
        public const int backLeftMotorID = 13;
        public const int backRightMotorID = 14;
    }


    /// Logictech 046d-c218 Rumblepad 2 USB
    public static class LogictechMapping
    {
        public const uint leftJoystickX = 0;
        public const uint leftJoystickY = 1;
        public const uint rightJoystickX = 2;
        public const uint rightJoystickY = 5;
        public const uint e1Button = 1; // xButton
        public const uint e4Button = 4; // yButton
        public const uint e3Button = 3; // bButton
        public const uint e2Button = 2; // aButton
        public const uint e5Button = 5; // LBButton
        public const uint e6Button = 6; // RBButton
        public const uint e7Button = 7; // LTButton
        public const uint e8Button = 8; // RTButton
        public const uint e9Button = 9; // Start
        public const uint e10Button = 10; // Settings

    }

    //Todo Make it work for Xbox360 GamePad. Map if nescessary.

    public static class HelperFunction {

        /**
         * If value is within 10% of center, clear it.
         * @param value [out] floating point value to deadband.
         */
        public static void Deadband(ref float value)
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
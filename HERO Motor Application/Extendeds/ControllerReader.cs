using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using Microsoft.SPOT;

namespace HERO_Motor_Application.Extendeds
{
    public class ControllerReader
    {
        // Controller
        GameController controller;

        // Controller Inputs
        float leftJoystickX;
        float leftJoystickY;
        float rightJoystickX;
        float rightJoystickY;
        bool xButton;
        bool yButton;
        bool aButton;
        bool bButton;
        public ControllerReader(GameController controller)
        {
            this.controller = controller;
        }


        /**
         *  Gets the raw inputs of the controller.
         */
         public void getCtrlInputs()
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


        public void printCtrl()
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

    }
}
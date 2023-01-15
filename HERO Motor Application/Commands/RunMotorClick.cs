using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.Tasking;
using HERO_Motor_Application.Extendeds;
using HERO_Motor_Application.Subsystems;
using Microsoft.SPOT;

namespace HERO_Motor_Application.Commands
{
    public class RunMotorClick : IProcessable, ILoopable
    {
        GameController controller;
        GenericActuator genericActuator;
        public RunMotorClick(GenericActuator genAct, GameController ctrl)
        {
            controller = ctrl;
            genericActuator = genAct;
        }
        public bool IsDone()
        {
            return !controller.GetButton(LogictechMapping.e1Button);
        }

        public void OnLoop()
        {
            Process();
            if (IsDone())
            {
                OnStop();
            };
        }

        public void OnStart()
        {

        }

        public void OnStop()
        {
            Debug.Print("STOP BUTTON ACTION");
            genericActuator.move(0);
        }

        public void Process()
        {
            Debug.Print("PROCESS BUTTON ACTION");
            genericActuator.move((float)0.5);
        }
    }



}
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.Tasking;
using Microsoft.SPOT;
using System;

namespace HERO_Motor_Application.Extendeds
{
    public class RobotState : StateMachine
    {
        GameController ctrl;
        Enum prevState;
        enum RobotStates
        {
            Disabled,
            Active,
            Test
        } 

        public RobotState(GameController ctrl) : base(RobotStates.Disabled)
        {
            this.ctrl = ctrl;
            this.prevState = RobotStates.Test;


        }

        public override void ProcessState(Enum currentState, int timeInStateMs)
        {
            // Get transition conditions;
            bool nextButtonClick = ctrl.GetButton(LogictechMapping.e9Button);
            bool prevButtonClick = ctrl.GetButton(LogictechMapping.e10Button);

            // Process States Evaluate Transition & Exits
            if  (currentState.Equals(RobotStates.Disabled))
            {
               
                if (nextButtonClick) 
                {
                    ChangeState(RobotStates.Active);
                    Debug.Print("Exit Disabled State");
                 };

                if (prevButtonClick)
                {
                    ChangeState(RobotStates.Test);
                    Debug.Print("Exit Disabled State");
                };


            }
            else if (currentState.Equals(RobotStates.Active))
            {

                if (nextButtonClick)
                {
                    ChangeState(RobotStates.Test);
                    Debug.Print("Exit Active State");
                };

                if (prevButtonClick)
                {
                    ChangeState(RobotStates.Disabled);
                    Debug.Print("Exit Active State");
                };
            }
            else if (currentState.Equals(RobotStates.Test))
            {

                if (nextButtonClick)
                {
                    ChangeState(RobotStates.Disabled);
                    Debug.Print("Exit Test State");
                };

                if (prevButtonClick)
                {
                    ChangeState(RobotStates.Active);
                    Debug.Print("Exit Test State");
                };
            }

            // Process States Entry 
            if (currentState.Equals(RobotStates.Disabled) && !currentState.Equals(prevState))
            {
                prevState = currentState;
                Debug.Print("Enter Disabled State");
            }
            else if (currentState.Equals(RobotStates.Active) && !currentState.Equals(prevState))
            {
                prevState = currentState;
                Debug.Print("Enter Active State");
            }
            else if (currentState.Equals(RobotStates.Test) && !currentState.Equals(prevState))
            {
                prevState = currentState;
                Debug.Print("Enter Test State");
            }

            // Process States Action
            if (currentState.Equals(RobotStates.Disabled))
            {
                Debug.Print("Do Disabled");
            }
            else if (currentState.Equals(RobotStates.Active))
            {
                Debug.Print("Do Active");
            }
            else if (currentState.Equals(RobotStates.Test))
            {
                Debug.Print("Do Test");
            }

        }
    }

}

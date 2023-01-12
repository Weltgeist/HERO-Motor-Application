using CTRE.Phoenix.Controller;
using CTRE.Phoenix.Tasking;
using System;

namespace HERO_Motor_Application.Extendeds
{
    public class RobotState : StateMachine
    {
        GameController ctrl;

        enum RobotStates
        {
            Disabled,
            Active,
            Test
        } 

        public RobotState(GameController ctrl) : base(RobotStates.Disabled)
        {
            this.ctrl = ctrl;
            
        }

        public override void ProcessState(Enum currentState, int timeInStateMs)
        {
            // Get transition conditions;
            bool nextButtonClick = ctrl.GetButton(LogictechMapping.e9Button);
            bool prevButtonClick = ctrl.GetButton(LogictechMapping.e10Button);

            // Process States
            if  (currentState.Equals(RobotStates.Disabled))
            {

                //Evaluate Transition conditions;
                if (nextButtonClick) 
                {
                    ChangeState(RobotStates.Active);
                 };

                if (nextButtonClick)
                {
                    ChangeState(RobotStates.Test);
                };
            }
            else if (currentState.Equals(RobotStates.Active))
            {

                //Evaluate Transition conditions;
                if (nextButtonClick)
                {
                    ChangeState(RobotStates.Test);
                };

                if (nextButtonClick)
                {
                    ChangeState(RobotStates.Disabled);
                };
            }
            else if (currentState.Equals(RobotStates.Test))
            {

                //Evaluate Transition conditions;
                if (nextButtonClick)
                {
                    ChangeState(RobotStates.Disabled);
                };

                if (nextButtonClick)
                {
                    ChangeState(RobotStates.Active);
                };
            }

        }
    }

}

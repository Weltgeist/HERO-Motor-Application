
using CTRE.Phoenix;
using CTRE.Phoenix.Tasking;

namespace HERO_Motor_Application.Commands
{
    public class FeedCTREWatchDog : IProcessable, ILoopable
    {
        public bool IsDone()
        {
            return false;
        }

        public void OnLoop()
        {
            Process();
        }

        public void OnStart()
        {

        }

        public void OnStop()
        {

        }

        public void Process()
        {
            /* feed watchdog to keep Talon's enabled , 100ms by default*/
            CTRE.Phoenix.Watchdog.Feed();
        }
    }
}
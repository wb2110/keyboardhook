using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace WorkBench
{
    class Throttle
    {
        bool flag;
        Timer timer;
        Action action;

        public Throttle(int ms)
        {
            timer = new Timer();
            timer.Interval = ms;
            timer.Elapsed += (s, e) =>
            {
                flag = false;
                timer.Stop();
                action.Invoke();
            };
        }



        public void start(Action act)
        {
            action = act;
            if (!flag)
            {
                flag = true;
                timer.Start();
            }
        }
    }
}

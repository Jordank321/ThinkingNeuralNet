using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using net;
using World;

namespace TrainingApp
{
    public class Simulation
    {
        private int _tickRate;

        public void Run()
        {
            var universe = new Universe(10, 10);
            universe.Place(new Agent("#", new KeyboardController(), ConsoleColor.Cyan), 5, 5);
            
            _tickRate = 5;

            var tickTimer = new Stopwatch();
            tickTimer.Start();
            while (true)
            {
                while (tickTimer.ElapsedMilliseconds < (1000/_tickRate))
                {
                    Thread.Sleep(1);
                }
                tickTimer.Reset();
                tickTimer.Start();

                universe.Update();
                universe.Draw();
            }
        }
    }
}

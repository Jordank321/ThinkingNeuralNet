using System;
using System.Collections.Generic;
using System.Text;

namespace World
{
    public interface IController
    {
        int InputX { get; }
        int InputY { get; }

        void UpdateInput();
    }
}

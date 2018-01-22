using System;
using System.Collections.Generic;
using System.Text;

namespace TickTacToe
{
    public interface IController
    {
        int Input { get; }
        int PlayerNumber { get; }

        void UpdateInput(string[,] board);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TickTacToe
{
    public class HumanController : IController
    {
        public int Input { get; private set; }
        public int PlayerNumber { get; }

        public HumanController(int playerNumber)
        {
            PlayerNumber = playerNumber;
        }

        private static readonly List<ConsoleKey> PossibleInputs = new List<ConsoleKey>()
        {
            ConsoleKey.NumPad1,
            ConsoleKey.NumPad2,
            ConsoleKey.NumPad3,
            ConsoleKey.NumPad4,
            ConsoleKey.NumPad5,
            ConsoleKey.NumPad6,
            ConsoleKey.NumPad7,
            ConsoleKey.NumPad8,
            ConsoleKey.NumPad9
        };

        public void UpdateInput(string[,] board)
        {
            while (true)
            {
                var input = Console.ReadKey(true);
                if (!PossibleInputs.Contains(input.Key)) continue;
                Input = input.KeyChar;
                break;
            }
        }
    }
}

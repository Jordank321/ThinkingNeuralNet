using System;

namespace TickTacToe
{
    public class Program
    {
        public static void Main()
        {
            var game = new TicTacToeGame(new HumanController(1), new HumanController(2));
            var winner = game.Play() == 1 ? "O" : "X";
            Console.WriteLine($"The winner is the {winner}'s!");
            Console.ReadKey();
        }
    }
}

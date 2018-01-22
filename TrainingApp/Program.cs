using System;
using System.Diagnostics;
using System.Threading;
using TickTacToe;
using TrainingApp;
using World;

namespace net
{
    public class Program
    {
        public static void Main()
        {
            var game = new TicTacToeGame(new NetControllerTicTacToe(1, new NeuralNetwork(10, 10, 18, 9), 100), new HumanController(2));
            game.Play();
        }
    }
}

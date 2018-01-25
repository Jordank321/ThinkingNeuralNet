using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            //var game = new TicTacToeGame(new NetControllerTicTacToe(1, new NeuralNetwork(10, 10, 18, 9), 100), new HumanController(2));

            var evolutionManager = new EvolutionManager(1000, 100, 1.00f, 5, 10, 18, 9);

            var epoch = 0;
            while (true)
            {
                epoch++;

                var survivingNetworks = new List<NeuralNetwork>();

                var startNetworks = evolutionManager.Networks;
                var games = new List<TicTacToeGame>();

                for (var i = 0; i < startNetworks.Count/2; i++)
                {
                    var network1 = startNetworks.ElementAt(i * 2);
                    var network2 = startNetworks.ElementAt(i * 2 + 1);

                    games.Add(
                        new TicTacToeGame(
                            new NetControllerTicTacToe(1, network1, 100),
                            new NetControllerTicTacToe(2, network2, 100)));
                }

                foreach (var game in games)
                {
                    var winner = game.Play();
                    
                    var netControllerTicTacToe = (NetControllerTicTacToe)game.GetController(winner);

                    
                    switch (game.WinningState)
                    {
                        case WinningState.NoEmptySpaces:
                            for (var i = 0; i < 10; i++)
                            {
                                survivingNetworks.Add(netControllerTicTacToe.GetNetwork());
                            }
                            break;

                        case WinningState.Foul:
                            for (var i = 0; i < 1; i++)
                            {
                                survivingNetworks.Add(netControllerTicTacToe.GetNetwork());
                            }
                            break;

                        case WinningState.ThreeInARow:
                            for (var i = 0; i < 20; i++)
                            {
                                survivingNetworks.Add(netControllerTicTacToe.GetNetwork());
                            }
                            break;
                    }
                }

                evolutionManager.SurvivingNetworks(survivingNetworks);
                Console.WriteLine("epoch: "+epoch);
            }
        }
    }
}

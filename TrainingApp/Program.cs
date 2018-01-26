using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
using Newtonsoft.Json;
using TickTacToe;
using TrainingApp;
using World;

namespace net
{
    public class Program
    {
        public static void Main()
        {
            var evolutionManager = new EvolutionManager(1000, 100, 1.00f, 5, 5, 18, 9);

            var epoch = 0;
            while (true)
            {
                if (epoch%50 == 0)
                {
                    using (var streamWriter = File.CreateText("C:\\evolution"+"\\"+epoch+".json"))
                    {
                        streamWriter.WriteAsync(JsonConvert.SerializeObject(evolutionManager)).GetAwaiter().GetResult();
                        streamWriter.Close();
                    }
                }

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

                var wins = 0;
                var fouls = 0;
                var stales = 0;
                foreach (var game in games)
                {
                    var winner = game.Play();

                    NetControllerTicTacToe netControllerTicTacToe;
                    if (winner != 0)
                    {
                        netControllerTicTacToe = (NetControllerTicTacToe)game.GetController(winner);
                    }
                    else
                    {
                        netControllerTicTacToe = (NetControllerTicTacToe)game.GetController(1);
                    }

                    
                    switch (game.WinningState)
                    {
                        case WinningState.NoEmptySpaces:
                            for (var i = 0; i < 2; i++)
                            {
                                survivingNetworks.Add(netControllerTicTacToe.GetNetwork());
                            }
                            stales++;
                            break;

                        case WinningState.Foul:
                            for (var i = 0; i < 1; i++)
                            {
                                survivingNetworks.Add(netControllerTicTacToe.GetNetwork());
                            }
                            fouls++;
                            break;

                        case WinningState.ThreeInARow:
                            for (var i = 0; i < 2; i++)
                            {
                                survivingNetworks.Add(netControllerTicTacToe.GetNetwork());
                            }

                            wins++;
                         break;
                    }
                }

                evolutionManager.SurvivingNetworks(survivingNetworks);
                Console.SetCursorPosition(0,7);
                Console.WriteLine("Stats from epoch: "+epoch);
                Console.WriteLine("Wins: "+wins);
                Console.WriteLine("Fouls: "+fouls);
                Console.WriteLine("Stalemates: "+stales);
                var stats = new Stats
                {
                    Epoch = epoch,
                    Fouls = fouls,
                    Stalemates = stales,
                    Wins = wins
                };

                using (var streamWriter = File.CreateText("C:\\evolution" + "\\" + epoch + "Stats.json"))
                {
                    streamWriter.WriteAsync(JsonConvert.SerializeObject(stats)).GetAwaiter().GetResult();
                    streamWriter.Close();
                }

                epoch++;
            }
        }
    }
}

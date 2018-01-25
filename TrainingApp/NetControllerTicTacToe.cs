using System;
using System.Collections.Generic;
using System.Text;
using net;
using TickTacToe;

namespace TrainingApp
{
    public class NetControllerTicTacToe : IController
    {
        private readonly NeuralNetwork _network;
        private readonly int _thinkingTicks;
        private readonly string _playerLetter;
        private readonly string _oppostionLetter;

        public int Input { get; private set; }
        public int PlayerNumber { get; }

        public NetControllerTicTacToe(int playerNumber, NeuralNetwork network, int thinkingTicks)
        {
            _network = network;
            _thinkingTicks = thinkingTicks;
            PlayerNumber = playerNumber;
            _playerLetter = playerNumber == 1 ? "O" : "X";
            _oppostionLetter = playerNumber == 1 ? "X" : "O";
        }

        public NeuralNetwork GetNetwork() => _network;

        public void UpdateInput(string[,] board)
        {
            //Map Inputs
            var inputs = _network.Inputs;
            for (var r = 0; r < board.GetLength(0); r++)
            {
                for (var c = 0; c < board.GetLength(1); c++)
                {
                    if (board[r, c] == _playerLetter)
                    {
                        inputs[r*3+c] = new Node{ Value = 1 };
                        inputs[r*3+c+9] = new Node{ Value = 0 };
                    }
                    else if (board[r, c] == _oppostionLetter)
                    {
                        inputs[r*3+c] = new Node{ Value = 0 };
                        inputs[r*3+c+9] = new Node{ Value = 1 };
                    }
                    else
                    {
                        inputs[r*3+c] = new Node{ Value = 0 };
                        inputs[r*3+c+9] = new Node{ Value = 0 };
                    }
                }
            }
            _network.Inputs = inputs;

            //Allow time to think
            for (int tick = 0; tick < _thinkingTicks; tick++)
            {
                _network.Update();
            }

            //Produce output
            var outputs = _network.Outputs;
            var maxvalue = -1f;
            var output = 0;
            for (var nodeIndex = 0; nodeIndex < outputs.Length; nodeIndex++)
            {
                if (!(outputs[nodeIndex].Value >= maxvalue)) continue;
                maxvalue = outputs[nodeIndex].Value;
                output = nodeIndex + 1;
            }

            Input = output.ToString().ToCharArray()[0];
        }
    }
}

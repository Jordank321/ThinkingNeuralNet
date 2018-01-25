using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace TickTacToe
{
    public class TicTacToeGame
    {
        private IController _player1Controller;
        private IController _player2Controller;
        private readonly string[,] _board;

        public WinningState WinningState { get; private set; }

        public TicTacToeGame(IController player1Controller, IController player2Controller)
        {
            _player1Controller = player1Controller;
            _player2Controller = player2Controller;
            _board = new string[3, 3];
            WinningState = WinningState.NoEmptySpaces;
        }

        public IController GetController(int playerNumber)
        {
            switch (playerNumber)
            {
                case 1:
                    return _player1Controller;
                case 2:
                    return _player2Controller;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void Draw()
        {
            Console.SetCursorPosition(0,0);
            Console.Write($" {_board[0,0] ?? " "} | {_board[0,1] ?? " "} | {_board[0,2] ?? " "}" + Environment.NewLine +
                          "-----------" + Environment.NewLine +
                          $" {_board[1,0] ?? " "} | {_board[1,1] ?? " "} | {_board[1,2] ?? " "}" + Environment.NewLine +
                          "-----------" + Environment.NewLine +
                          $" {_board[2,0] ?? " "} | {_board[2,1] ?? " "} | {_board[2,2] ?? " "}" + Environment.NewLine);
        }

        public int Play()
        {
            var winner = 0;
            var i = 1;
            var playerToGo = _player1Controller;
            
            Draw();

            while (winner == 0 && i <= 9)
            {
                playerToGo.UpdateInput(_board);

                var input = MapInput(playerToGo.Input);
                if (_board[input.Item1, input.Item2] == null)
                {
                    _board[input.Item1, input.Item2] = playerToGo.PlayerNumber == 1 ? "O" : "X";
                    winner = CheckForWin(_board);
                    if (winner != 0)
                    {
                        Draw();
                        WinningState = WinningState.ThreeInARow;
                        Console.WriteLine("3 in a row!");
                    }
                }
                else
                {
                    winner = playerToGo.PlayerNumber == 1 ? 2 : 1;
                    WinningState = WinningState.Foul;
                }

                if (playerToGo.PlayerNumber == 1)
                {
                    _player1Controller = playerToGo;
                    playerToGo = _player2Controller;
                }
                else
                {
                    _player2Controller = playerToGo;
                    playerToGo = _player1Controller;
                }

                Draw();
                i++;
            }

            return winner;
        }

        private int CheckForWin(string[,] board)
        {
            //Check rows
            for (int i = 0; i < 3; i++)
            {
                if (_board[i,0] == _board[i,1] && _board[i,0] == _board[i,2] && _board[i,0] != null)
                {
                    return _board[i, 0] == "O" ? 1 : 2;
                }
            }
            //Check Columns
            for (int i = 0; i < 3; i++)
            {
                if (_board[0,i] == _board[1,i] && _board[0,i] == _board[2,i] && _board[0,i] != null)
                {
                    return _board[0, i] == "O" ? 1 : 2;
                }
            }
            //Check Diagonals
            if (_board[0,0] == _board[1,1] && _board[0,0] == _board[2,2] && _board[0,0] != null)
            {
                return _board[0, 0] == "O" ? 1 : 2;
            }
            if (_board[0,2] == _board[1,1] && _board[0,2] == _board[2,0] && _board[0,2] != null)
            {
                return _board[0, 0] == "O" ? 1 : 2;
            }
            return 0;
        }

        private static (int, int) MapInput(int input)
        {
            switch (input)
            {
                case '1':
                    return (2, 0);
                case '2':
                    return (2, 1);
                case '3':
                    return (2, 2);
                case '4':
                    return (1, 0);
                case '5':
                    return (1, 1);
                case '6':
                    return (1, 2);
                case '7':
                    return (0, 0);
                case '8':
                    return (0, 1);
                case '9':
                    return (0, 2);
            }
            throw new InvalidOperationException();
        }
    }
}

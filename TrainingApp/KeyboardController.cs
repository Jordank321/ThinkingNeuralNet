using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using World;

namespace net
{
    public class KeyboardController : IController
    {
        public int InputX { get; private set; }
        public int InputY { get; private set; }

        private bool _upPressed;
        private bool _downPressed;
        private bool _leftPressed;
        private bool _rightPressed;

        public KeyboardController()
        {
            Task.Factory.StartNew(this.Listen);
        }

        public void UpdateInput()
        {
            InputX = (_rightPressed ? 1 : 0) - (_leftPressed ? 1 : 0);
            InputY = (_upPressed ? 1 : 0) - (_downPressed ? 1 : 0);

            _upPressed = false;
            _downPressed = false;
            _leftPressed = false;
            _rightPressed = false;
        }

        private void Listen()
        {
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        _upPressed = true;
                        break;
                    case ConsoleKey.S:
                        _downPressed = true;
                        break;
                    case ConsoleKey.A:
                        _leftPressed = true;
                        break;
                    case ConsoleKey.D:
                        _rightPressed = true;
                        break;
                }
            }
        }
    }
}

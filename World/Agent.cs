using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace World
{
    public class Agent : UniverseObject
    {
        private readonly string _drawSymbol;
        private readonly IController _controller;
        private readonly ConsoleColor _color;

        public Agent(string drawSymbol, IController controller, ConsoleColor color): base(true)
        {
            _drawSymbol = drawSymbol;
            _controller = controller;
            _color = color;
        }

        public override void Update(UniverseObject[,] universe, int x, int y)
        {
            _controller.UpdateInput();

            var targetX = x + _controller.InputX;
            var targetY = y + _controller.InputY;

            if ((targetX != x ||
                targetY != y) &&
                targetX >= 0 &&
                targetX < universe.GetLength(0) &&
                targetY >= 0 &&
                targetY < universe.GetLength(1))
            {
                universe[x, y] = null;
                universe[targetX, targetY] = this;
            }
        }

        public override void Draw()
        {
            Console.ForegroundColor = _color;
            Console.Write(_drawSymbol);
        }
    }
}

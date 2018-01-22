using System;

namespace World
{
    public class Universe
    {
        private readonly int _width;
        private readonly int _height;
        private UniverseObject[,] _objects;
        private int _time;

        public Universe(int width, int height)
        {
            _time = 0;

            _width = width;
            _height = height;
            _objects = new UniverseObject[width,height];
        }

        public void Place(UniverseObject universeObject, int x, int y)
        {
            _objects[x, y] = universeObject;
        }

        public void Update()
        {
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    var tempExistance = _objects[x, y];
                    if (tempExistance == null || !tempExistance.Updateable) continue;
                    
                    tempExistance.Update(_objects, x, y);
                }
            }

            _time++;
        }

        public void Draw()
        {
            Console.SetCursorPosition(0,0);
            Console.CursorVisible = false;
            for (var y = _height-1; y >= 0; y--)
            {
                for (var x = 0; x < _width; x++)
                {
                    var tempExistance = _objects[x, y];
                    if (tempExistance == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("~");
                        continue;
                    }
                    
                    tempExistance.Draw();
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}

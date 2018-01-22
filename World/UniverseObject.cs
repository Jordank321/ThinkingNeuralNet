using System;
using System.Collections.Generic;
using System.Text;

namespace World
{
    public abstract class UniverseObject
    {
        public bool Updateable { get; private set; }

        protected UniverseObject(bool updateable)
        {
            Updateable = updateable;
        }

        public abstract void Update(UniverseObject[,] universe, int x, int y);

        public abstract void Draw();
    }
}

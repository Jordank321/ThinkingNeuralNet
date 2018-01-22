using System;
using System.Collections.Generic;
using System.Text;

namespace net
{
    public class Node
    {
        public float Value { get; set; }
        public List<Connection> Connections { get; set; }

        public Node()
        {
            Value = 0;
            Connections = new List<Connection>();
        }
    }
}

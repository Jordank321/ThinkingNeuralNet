using System;
using System.Collections.Generic;
using System.Text;

namespace net
{
    public class Connection
    {
        public float Weight { get; set; }
        public int FromNodeLayerIndex { get; set; }
        public int FromNodeIndex { get; set; }
    }
}

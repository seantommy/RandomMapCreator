using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class Map
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public char[,] Contents { get; set; }

        public Map() { }
        public Map(int setHeight, int setWidth)
        {
            Height = setHeight;
            Width = setWidth;
            Contents = new char[Height, Width];
        }
    }
}

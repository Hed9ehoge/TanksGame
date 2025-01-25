using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksGame.Struct
{
    public struct Cell
    {
        public int X;
        public int Y;
        public bool Equals(Cell cell)
        {
            if (cell.X == X && cell.Y == Y)
                return true;
            return false;
        }
        public Cell Sum(Cell cell)
        {
            return new Cell(cell.X + X, cell.Y + Y);
        }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
